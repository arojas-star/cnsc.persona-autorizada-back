using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IVessperienciasRepository
    {
        Task<List<VessperienciasAreaResponse>> GetAreas();
        Task<List<VessperienciasPersonalResponse>> GetPersonalXArea(int CodigoArea);
        Task<VessperienciasPersonal?> GetPersonal(int CodigoPersonal);
        Task<List<VessperienciasTemaResponse>> GetPonencias();
        Task<string> GetTema(List<int> CodigosPonencia);
        Task<List<VessperienciasPonencia>> ValidatePonencias(List<int> CodigosPonencia);
        Task GuardarInscripcion(VessperienciasDto dto);
    }
}
