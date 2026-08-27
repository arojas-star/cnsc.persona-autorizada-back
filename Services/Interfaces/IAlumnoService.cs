using personaAutorizadaBack.Dtos;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IAlumnoService
    {
        Task<InformacionAlumnDto?> ObtenerDetalleAlumno(int TipoDocumento, string NumeroDocumento);
        Task<List<AlumnoResumenDto>> ObtenerHermanosAlumnos(string CodigoAlumno);
        Task<List<TipoDocumentoResumenDto>> ObtenerTiposDocumentos();
    }
}
