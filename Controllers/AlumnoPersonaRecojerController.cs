using Microsoft.AspNetCore.Mvc;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlumnoPersonaRecojerController : ControllerBase
    {
        private IAlumnoPersonaRecojoService _service;
        public AlumnoPersonaRecojerController(IAlumnoPersonaRecojoService personaRecojoService)
        {
            _service = personaRecojoService;
        }

        [HttpPost(Name = "AlumnoPersonaRecojer")]
        public async Task<ActionResult<AlumnoPersonaRecojoDto>> RegistrarPersonasRecojer([FromBody] AlumnoPersonaRecojoDto dto)
        {
            var resultado = await _service.RegistrarPersonaRecojo(dto);

            if (!resultado.Success)
            {
                return StatusCode(
                    resultado.StatusCode,
                    new
                    {
                        mensaje = resultado.Error
                    }
                );
            }

            return Ok(resultado.Data);
        }
    }
}
