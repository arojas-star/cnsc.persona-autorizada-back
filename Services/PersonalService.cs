using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Services
{
    public class PersonalService : IPersonalService
    {
        private readonly IPersonalRepository _repository;

        public PersonalService(IPersonalRepository repository)
        {
            _repository = repository;
        }

        public async Task UpdatePersonal(int CodigoPersonal, string Correo, string Celular)
        {
            await _repository.UpdateCorreo(CodigoPersonal, Correo);
            await _repository.UpdateCelular(CodigoPersonal, Celular);
        }

        public async Task<EventoPapaResponse?> GetPersonal(int CodigoPersonal)
        {
            var personal = await _repository.GetPersonal(CodigoPersonal);

            if (personal == null)
            {
                return null;
            }

            int? Edad = null;

            if (personal != null && personal.FechaNacimiento.HasValue && personal.FechaNacimiento != null)
            {
                DateTime hoy = DateTime.Today;

                int edad = hoy.Year - (personal.FechaNacimiento?.Year ?? 0);

                if (personal.FechaNacimiento?.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }

                Edad = edad;
            }

            return new EventoPapaResponse
            {
                CodigoPersonal = personal.CodigoPersonal!,
                NumeroDocumento = personal.NumeroDocumento!,
                Correo = personal.Correo!,
                NombreCompleto = personal.NombreCompleto!,
                Edad = Edad,
                Celular = personal.Celular,
                FechaNacimiento = personal.FechaNacimiento?.ToString("dd/MM/yyyy") ?? ""
            };
        }

        public async Task<EventoPapaResponse?> GetPersonalByDocument(EventoPapaPersonalRequest dto)
        {
            var personal = await _repository.GetPersonalByDocument(dto.TipoDocumento, dto.NumeroDocumento);

            if (personal == null) return null;

            int? Edad = null;

            if (personal != null && personal.FechaNacimiento.HasValue && personal.FechaNacimiento != null)
            {
                DateTime hoy = DateTime.Today;

                int edad = hoy.Year - (personal.FechaNacimiento?.Year ?? 0);

                if (personal.FechaNacimiento?.Date > hoy.AddYears(-edad))
                {
                    edad--;
                }

                Edad = edad;
            }

            return new EventoPapaResponse
            {
                CodigoPersonal = personal.CodigoPersonal!,
                NumeroDocumento = personal.NumeroDocumento!,
                Correo = personal.Correo!,
                NombreCompleto = personal.NombreCompleto!,
                Edad = Edad,
                Celular = personal.Celular,
                FechaNacimiento = personal.FechaNacimiento?.ToString("dd/MM/yyyy") ?? ""
            };
        }
    }
}
