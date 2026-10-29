using Microsoft.AspNetCore.Mvc;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Controllers
{
    [ApiController]
    [Route("Sharingday")]
    public class SharingDayController : ControllerBase
    {
        private readonly ISharingDayService _service;

        public SharingDayController(ISharingDayService service)
        {
            _service = service;
        }

        [HttpPost("Persona")]
        public async Task<IActionResult> GetParent([FromBody] ParentSharingDayRequest dto)
        {
            var (status, data) = await _service.FindParent(dto);

            if (data == null)
            {
                return NotFound(new
                {
                    message = "No se encontró una persona con este número de documento."
                });
            }

            return Ok(data);
        }

        [HttpGet("Horario/{CodigoGrado:int}/{CodigoSeccion:int}")]
        public async Task<IActionResult> GetTurns([FromRoute] int CodigoGrado, [FromRoute] int CodigoSeccion)
        {
            var result = await _service.GetTurns(CodigoGrado, CodigoSeccion);

            return Ok(result);
        }

        [HttpPost("Inscripcion")]
        public async Task<IActionResult> Register(RegisterSharingDayRequest dto)
        {
            var (status, message) = await _service.RegisterParent(dto);

            if (status == null)
            {
                return NotFound(new
                {
                    message
                });
            }

            if (status == false)
            {
                return BadRequest(new
                {
                    message
                });
            }

            return NoContent();
        }
    }
}
