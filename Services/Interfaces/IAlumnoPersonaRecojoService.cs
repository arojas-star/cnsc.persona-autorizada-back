using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IAlumnoPersonaRecojoService
    {
        Task<ServiceResult<AlumnoPersonaRecojoDto>> RegistrarPersonaRecojo(AlumnoPersonaRecojoDto personaRecojoDto);
    }
}
