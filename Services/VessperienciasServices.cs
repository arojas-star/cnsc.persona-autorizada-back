using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Services
{
    public class VessperienciasServices: IVessperienciasServices
    {
        private IVessperienciasRepository _vessperienciasRepository;

        public VessperienciasServices(IVessperienciasRepository vessperienciasRepository)
        {
            _vessperienciasRepository = vessperienciasRepository;
        }

        public async Task<List<VessperienciasAreaResponse>> GetAreas()
        {
            return await _vessperienciasRepository.GetAreas();
        }

        public async Task<List<VessperienciasPersonalResponse>> GetPersonal(int CodigoArea)
        {
            return await _vessperienciasRepository.GetPersonalXArea(CodigoArea);
        }

        public async Task<VessperienciasPersonal?> GetPersonalXCodigo(int CodigoPersonal)
        {
            return await _vessperienciasRepository.GetPersonal(CodigoPersonal);
        }

        public async Task<List<VessperienciasTemaResponse>> GetPonencias()
        {
            return await _vessperienciasRepository.GetPonencias();
        }

        public async Task<string> GetPonenciasTemas(List<int> CodigosPonencia)
        {
            return await _vessperienciasRepository.GetTema(CodigosPonencia);
        }

        public async Task<List<VessperienciasPonencia>> ValidatePonencias(List<int> CodigosPonencia)
        {
            return await _vessperienciasRepository.ValidatePonencias(CodigosPonencia);
        }

        public async Task SaveInscripcion(VessperienciasDto dto)
        {
            await _vessperienciasRepository.GuardarInscripcion(dto);
        }
    }
}
