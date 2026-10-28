using Microsoft.AspNetCore.Mvc;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class EventoPapaController : ControllerBase
    {
        private readonly IPersonalService _personalService;
        private readonly IEventoPapaService _eventoPapaService;

        public EventoPapaController(IPersonalService personalService, IEventoPapaService eventoPapaService)
        {
            _personalService = personalService;
            _eventoPapaService = eventoPapaService;
        }

        [HttpGet("Personal/{CodigoTipoDocumento:int}/{NumeroDocumento}")]
        public async Task<IActionResult> GetPersonal([FromRoute] int CodigoTipoDocumento, [FromRoute] string NumeroDocumento)
        {
            var response = await _personalService.GetPersonalByDocument(new EventoPapaPersonalRequest
            {
                NumeroDocumento = NumeroDocumento,
                TipoDocumento = CodigoTipoDocumento
            });

            if (response == null)
            {
                return NotFound(new { message = "No se encontro un Personal válido." });
            }
            return Ok(response);
        }

        [HttpGet("Horario")]
        public async Task<IActionResult> GetSchedules()
        {
            var response = await _eventoPapaService.GetHorarios();

            return Ok(response);
        }

        [HttpPost("Inscripcion")]
        public async Task<IActionResult> RegisterInscription([FromBody] EventoPapaInscripcionRequest dto)
        {
            var (status, message) = await _eventoPapaService.RegisterInscription(dto);

            if (status == null)
            {
                return NotFound(new
                {
                    message = message,
                });
            }

            if (status == false)
            {
                return BadRequest(new
                {
                    message = message,
                });
            }

            return NoContent();
        }
    }
}
