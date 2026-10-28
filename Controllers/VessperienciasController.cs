using Microsoft.AspNetCore.Mvc;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VessperienciasController : ControllerBase
    {
        private readonly IVessperienciasServices _vessperienciasService;

        public VessperienciasController(IVessperienciasServices vessperienciasServices)
        {
            _vessperienciasService = vessperienciasServices;
        }

        [HttpGet("area")]
        public async Task<IActionResult> GetAreas()
        {
            var areas = await _vessperienciasService.GetAreas();

            return Ok(areas);
        }

        [HttpGet("personal/{CodigoArea:int}")]
        public async Task<IActionResult> GetPersonal([FromRoute] int CodigoArea)
        {
            var personal = await _vessperienciasService.GetPersonal(CodigoArea);

            return Ok(personal);
        }

        [HttpGet("ponencia/{CodigoTurno:int}")]
        public async Task<IActionResult> GetPonencias([FromRoute] int CodigoTurno)
        {
            var ponencias = await _vessperienciasService.GetPonencias(CodigoTurno);

            return Ok(ponencias);
        }

        [HttpPost("inscripcion")]
        public async Task<IActionResult> SaveInscripcion(VessperienciasDto dto)
        {
            bool noHayRepetidos = dto.temas.Count == dto.temas.Distinct().Count();

            if (!noHayRepetidos)
            {
                return BadRequest(new
                {
                    mensaje = "Debe seleccionar temas diferentes para cada horario, por favor intentelo nuevamente."
                });
            }

            var personal = await _vessperienciasService.GetPersonalXCodigo(dto.codigoPersonal);

            if (personal == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró los datos del personal que esta intentando registrar"
                });
            }

            var temas = await _vessperienciasService.ValidatePonencias(dto.temas);

            if (dto.temas.Count != temas.Count)
            {
                List<int> diferencia = dto.temas.Select(x => x.codigoTema).ToList()
                    .Except(temas.Select(o => o.CodigoPonencia))
                    .ToList();

                var temasDescripcion = await _vessperienciasService.GetPonenciasTemas(diferencia);

                string mensaje = $"Los siguientes temas ya se encuentran llenos, por favor elegir otra opción: {temasDescripcion}";

                return BadRequest(new
                {
                    mensaje
                });
            }

            try
            {
                await _vessperienciasService.SaveInscripcion(dto);

                return NoContent();
            }
            catch
            {
                return BadRequest(new
                {
                    mensaje = "Ocurrió un problema al intentar registrarlo, por favor intentelo nuevamente en unos minutos."
                });
            }
        }
    }
}
