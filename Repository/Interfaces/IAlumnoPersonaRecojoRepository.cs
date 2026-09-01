using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IAlumnoPersonaRecojoRepository
    {
        Task<List<AlumnoPersonaRecojo>> ExisteRegistroXAlumno(string CodigoAlumno);
        Task<AlumnoPersonaRecojo?> ExisteRegistroXAlumnoPersonaAutorizada(string CodigoAlumno, int TipoDocumentoPersonaAutorizada, string NumeroDocumentoPersonaAutorizada);
        Task<AlumnoPersonaRecojo> RegistrarPersona(AlumnoPersonaRecojo alumnoRecojosDto);
        Task RegistrarPersonasRecojo(List<AlumnoPersonaRecojo> alumnoRecojosDto);
    }
}
