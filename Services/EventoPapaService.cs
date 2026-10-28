using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Services
{
    public class EventoPapaService : IEventoPapaService
    {
        private readonly IEventoPapaRepository _repository;
        private readonly IPersonalService _personalService;

        public EventoPapaService(IEventoPapaRepository repository, IPersonalService personalService)
        {
            _repository = repository;
            _personalService = personalService;
        }

        public async Task<List<EventoPapaHorarioResponse>> GetHorarios()
        {
            var horarios = await _repository.GetSchedules();

            return horarios.Select((h, i) => new EventoPapaHorarioResponse
            {
                CantidadCupos = (int)h.Vacantes!,
                codigoEvento = h.CodigoHorarioEventoPapa,
                Descripcion = h.Descripcion!,
                MaxEdad = h.MaxEdad,
                MinEdad = h.MinEdad,
                Order = i + 1
            }).ToList();
        }

        public async Task<(bool?, string)> RegisterInscription(EventoPapaInscripcionRequest dto)
        {
            var personal = await _personalService.GetPersonal(dto.CodigoPersonal);

            if (personal == null)
            {
                return (null, "No se encontró un Personal válido.");
            }

            var exist = await _repository.ExistInscription(dto.CodigoPersonal);

            if (exist != null)
            {
                return (false, "Ya se encuentra registrado.");
            }

            var schedule = await _repository.GetSchedule(dto.CodigoEvento);

            if (schedule == null)
            {
                return (null, "No se encontró un Horario válido.");
            }

            var spotstake = await _repository.GetSpotsTake(dto.CodigoEvento);

            if (spotstake >= schedule.Vacantes)
            {
                return (false, "No hay cupos disponibles para este horario.");
            }

            int? EdadPersonal = dto.Edad != null ? dto.Edad : personal.Edad;

            if (schedule.MinEdad.HasValue && EdadPersonal.HasValue && schedule.MinEdad >= EdadPersonal)
            {
                return (false, "No cuenta con la edad requerida. Por favor verificar las condiciones.");
            }

            if (schedule.MaxEdad.HasValue && EdadPersonal.HasValue && schedule.MaxEdad < EdadPersonal)
            {
                return (false, "No cuenta con la edad requerida. Por favor verificar las condiciones.");
            }

            if (dto.Correo != null && dto.Celular != null)
            {
                await _personalService.UpdatePersonal(personal.CodigoPersonal, dto.Correo, dto.Celular);
            }

            EventoPapa data = new()
            {
                CodigoHorarioEventoPapa = dto.CodigoEvento,
                CodigoPersonal = dto.CodigoPersonal,
                FechaRegistro = DateTime.Now
            };

            await _repository.RegisterInscripcion(data);

            return (true, "Correcto");
        }
    }
}
