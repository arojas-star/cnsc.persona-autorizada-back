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

        public InformacionPersonaController(
            IApiExternaService apiExternaService)
        {
            _apiExternaService = apiExternaService;
        }

        [HttpGet("{NumeroDocumento}")]
        public async Task<IActionResult> ObtenerInfoPersona(string NumeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(NumeroDocumento))
            {
                return BadRequest("El Numero de Documento es obligatorio");
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
                NumeroDocumento = NumeroDocumento,
                Nombres = resultado.Data.Nombres,
                ApellidoPaterno = resultado.Data.ApellidoPaterno,
                ApellidoMaterno = resultado.Data.ApellidoMaterno
            });
        }
    }
}
