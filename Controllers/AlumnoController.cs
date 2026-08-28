using Microsoft.AspNetCore.Mvc;
using personaAutorizadaBack.Services;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;

        public AlumnoController(
            IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }

        [HttpGet("Detalles/{TipoDocumento}/{NumeroDocumento}")]
        public async Task<IActionResult> ObtenerInfoAlumno(int TipoDocumento, string NumeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(NumeroDocumento))
            {
                return BadRequest("El Numero de Documento es obligatorio");
            }

            var resultado = await _alumnoService.ObtenerDetalleAlumno(TipoDocumento, NumeroDocumento);

            if (resultado is null)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró información para el Número de Documento {NumeroDocumento}"
                });
            }

            return Ok(resultado);
        }

        [HttpGet("TipoDocumento")]
        public async Task<ActionResult> ObtenerTipoDocumentos()
        {
            var tiposDocumento = await _alumnoService.ObtenerTiposDocumentos();

            return Ok(new {
                TipoDocumento = tiposDocumento
            });
        }
    }
}
