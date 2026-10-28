using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IVessperienciasRepository
    {
        Task<List<VessperienciasAreaResponse>> GetAreas();
        Task<List<VessperienciasPersonalResponse>> GetPersonalXArea(int CodigoArea);
        Task<VessperienciasPersonal?> GetPersonal(int CodigoPersonal);
        Task<List<VessperienciasTemaResponse>> GetPonencias(int CodigoTurno);
        Task<string> GetTema(List<int> CodigosPonencia);
        Task<VessperienciasPonencia?> ValidatePonencias(int CodigosPonencia, int? CodigoTurno);
        Task GuardarInscripcion(VessperienciasDto dto);
    }
}
