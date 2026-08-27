using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IAlumnoPersonaRecojoRepository
    {
        Task<List<AlumnoPersonaRecojo>> ExisteRegistroXAlumno(string CodigoAlumno);
        Task<AlumnoPersonaRecojo> RegistrarPersona(AlumnoPersonaRecojo alumnoRecojosDto);
        Task RegistrarPersonasRecojo(List<AlumnoPersonaRecojo> alumnoRecojosDto);
    }
}
