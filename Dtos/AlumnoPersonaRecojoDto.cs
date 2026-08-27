using System.ComponentModel.DataAnnotations;

namespace personaAutorizadaBack.Dtos
{
    public class AlumnoPersonaRecojoDto
    {
        [Required(ErrorMessage = "El codigo del Alumno es obligatorio", AllowEmptyStrings = false)]
        public string CodigoAlumno { get; set; }

        [Required(ErrorMessage = "Es obligatorio al menos una persona encargada")]
        [MinLength(1, ErrorMessage = "Debe existir al menos una persona encargada")]
        [MaxLength(3, ErrorMessage = "No puede haber más de 3 personas encargadas")]
        public List<PersonaRecojoDto> personaRecojo { get; set; } = [];
        public List<string>? codigoAlumnosParientes { get; set; }
    }

    public class PersonaRecojoDto
    {
        [Required(ErrorMessage = "El Tipo de documento de la persona encargada es necesaria")]
        public int TipoDocumento { get; set; }

        [Required(ErrorMessage = "El Número de documento de la persona encargada es necesaria", AllowEmptyStrings = false)]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El Apellido Paterno de la persona encargada es necesaria", AllowEmptyStrings = false)]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El Apellido Materno de la persona encargada es necesaria", AllowEmptyStrings = false)]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "El Nombre de la persona encargada es necesaria", AllowEmptyStrings = false)]
        public string Nombres { get; set; }
    }
}
