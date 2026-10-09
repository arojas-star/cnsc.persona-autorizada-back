using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IVessperienciasServices
    {
        Task<List<VessperienciasAreaResponse>> GetAreas();
        Task<List<VessperienciasPersonalResponse>> GetPersonal(int CodigoArea);
        Task<VessperienciasPersonal?> GetPersonalXCodigo(int CodigoPersonal);
        Task<List<VessperienciasTemaResponse>> GetPonencias();
        Task<string> GetPonenciasTemas(List<int> CodigosPonencia);
        Task<List<VessperienciasPonencia>> ValidatePonencias(List<int> CodigosPonencia);
        Task SaveInscripcion(VessperienciasDto dto);
    }
}
