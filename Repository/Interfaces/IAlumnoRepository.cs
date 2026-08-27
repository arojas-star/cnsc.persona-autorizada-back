using personaAutorizadaBack.Dtos;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IAlumnoRepository
    {
        Task<AlumnoResumenDto?> ExisteAlumnoPorCodigo(string codigoAlumno);
        Task<AlumnoResumenDto?> ExisteAlumnoPorTipoDocumentoNumeroDocumento(int TipoDocumento, string NumeroDocumento);
        Task<List<AlumnoResumenDto>> ObtenerHermanosAlumnos(string CodigoAlumno);
        Task<List<TipoDocumentoResumenDto>> ObtenerListaTiposDocumentos();
    }
}
