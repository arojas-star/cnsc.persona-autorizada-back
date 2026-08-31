using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Services
{
    public class ParienteService : IParienteService
    {
        private IParienteRepository _parienteRepository;

        public ParienteService(IParienteRepository parienteRepository)
        {
            _parienteRepository = parienteRepository;
        }

        public async Task<PadreResumenDto?> ObtenerParienteXTipoNumeroDocumento(int CodigoTipoDocumento, string NumeroDocumento)
        {
            var pariente = await _parienteRepository.ExistePadreXTipoNumeroDocumento(CodigoTipoDocumento, NumeroDocumento);

            return pariente;
        }
    }
}
