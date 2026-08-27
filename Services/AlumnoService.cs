using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Services
{
    public class AlumnoService : IAlumnoService
    {
        private IAlumnoRepository _alumnoRepository;

        public AlumnoService(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        public async Task<InformacionAlumnDto?> ObtenerDetalleAlumno(int TipoDocumento, string NumeroDocumento)
        {
            AlumnoResumenDto? alumno = await _alumnoRepository.ExisteAlumnoPorTipoDocumentoNumeroDocumento(TipoDocumento, NumeroDocumento);

            if (alumno == null) {
                return null;
            }

            var hermanos = await ObtenerHermanosAlumnos(alumno.CodigoAlumno);

            return new InformacionAlumnDto { AlumnoResumen = alumno, Parientes = hermanos };
        }

        public async Task<List<AlumnoResumenDto>> ObtenerHermanosAlumnos(string CodigoAlumno)
        {
            var hermanos = await _alumnoRepository.ObtenerHermanosAlumnos(CodigoAlumno);

            return hermanos ?? [];
        }

        public async Task<List<TipoDocumentoResumenDto>> ObtenerTiposDocumentos()
        {
            return await _alumnoRepository.ObtenerListaTiposDocumentos();
        }
    }
}
