using Microsoft.AspNetCore.Mvc;
using personaAutorizadaBack.Services.Interfaces;
using System.Net;

namespace personaAutorizadaBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InformacionPersonaController : ControllerBase
    {
        private readonly IApiExternaService _apiExternaService;
        private readonly IParienteService _parienteService;

        public InformacionPersonaController(
            IApiExternaService apiExternaService, IParienteService parienteService)
        {
            _apiExternaService = apiExternaService;
            _parienteService = parienteService;
        }

        [HttpGet("{CodigoTipoDocumento}/{NumeroDocumento}")]
        public async Task<IActionResult> ObtenerInfoPersona(int CodigoTipoDocumento, string NumeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(NumeroDocumento))
            {
                return BadRequest("El Numero de Documento es obligatorio");
            }

            var existeBd = await _parienteService.ObtenerParienteXTipoNumeroDocumento(CodigoTipoDocumento, NumeroDocumento);

            if (existeBd is not null)
            {
                return Ok(new
                {
                    CodigoTipoDocumento = existeBd.CodigoTipoDocumento,
                    TipoDocumentoTexto = existeBd.TipoDocumentoTexto,
                    NumeroDocumento = NumeroDocumento,
                    Nombres = existeBd.Nombre,
                    ApellidoPaterno = existeBd.ApellidoPaterno,
                    ApellidoMaterno = existeBd.ApellidoMaterno,
                });
            }

            if (CodigoTipoDocumento != 2)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró información para el Número de documento: {NumeroDocumento}"
                });
            }

            var resultado = await _apiExternaService.ObtenerInfoPersona(NumeroDocumento);

            if (resultado is null)
            {
                return NotFound(new
                {
                    mensaje = $"No se encontró información para el Número de documento: {NumeroDocumento}"
                });
            }

            if (resultado.Data is null)
            {
                return NotFound($"No se encontró los datos con el documento: {NumeroDocumento}");
            }

            return Ok(new
            {
                CodigoTipoDocumento = 2,
                TipoDocumentoTexto = "D N I",
                NumeroDocumento = NumeroDocumento,
                Nombres = resultado.Data.Nombres,
                ApellidoPaterno = resultado.Data.ApellidoPaterno,
                ApellidoMaterno = resultado.Data.ApellidoMaterno
            });
        }
    }
}
