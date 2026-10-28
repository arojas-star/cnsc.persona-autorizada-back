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

            if (schedule.MinEdad.HasValue && personal.Edad.HasValue && schedule.MinEdad >= personal.Edad)
            {
                return (false, "No cuenta con la edad requerida. Por favor verificar las condiciones.");
            }

            if (schedule.MaxEdad.HasValue && personal.Edad.HasValue && schedule.MaxEdad < personal.Edad)
            {
                return (false, "No cuenta con la edad requerida. Por favor verificar las condiciones.");
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
