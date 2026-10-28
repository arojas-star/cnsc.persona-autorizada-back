using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IVessperienciasServices
    {
        Task<List<VessperienciasAreaResponse>> GetAreas();
        Task<List<VessperienciasPersonalResponse>> GetPersonal(int CodigoArea);
        Task<VessperienciasPersonal?> GetPersonalXCodigo(int CodigoPersonal);
        Task<List<VessperienciasTemaResponse>> GetPonencias(int CodigoTurno);
        Task<string> GetPonenciasTemas(List<int> CodigosPonencia);
        Task<List<VessperienciasPonencia>> ValidatePonencias(List<VessperienciasTemaDto> CodigosPonencia);
        Task SaveInscripcion(VessperienciasDto dto);
    }
}
