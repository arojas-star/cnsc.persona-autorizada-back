using System.ComponentModel.DataAnnotations;

namespace personaAutorizadaBack.Dtos
{
    public class VessperienciasTemaDto
    {
        [Required(ErrorMessage = "El codigo del Personal es obligatorio")]
        public int codigoTema { get; set; }

        [Required(ErrorMessage = "El codigo del Personal es obligatorio")]
        public int codigoTurno { get; set; }
    }

    public class VessperienciasDto
    {
        [Required(ErrorMessage = "El codigo del Personal es obligatorio")]
        public int codigoPersonal { get; set; }

        [Required(ErrorMessage = "Es obligatorio al menos un tema")]
        [MinLength(2, ErrorMessage = "Es obligatorio registrarse en solo 2 temas")]
        [MaxLength(2, ErrorMessage = "Es obligatorio registrarse en solo 2 temas")]
        public List<VessperienciasTemaDto> temas { get; set; } = [];
    }
}
