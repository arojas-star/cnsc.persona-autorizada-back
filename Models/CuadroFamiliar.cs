using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("CuadroFamiliar", Schema = "admision")]
    public class CuadroFamiliar
    {
        public string CodigoFamilia { get; set; } = null!;

        public string CodigoPariente { get; set; } = null!;

        // Corresponde al CodigoAlumno de la tabla Alumno
        public string CodigoPostulante { get; set; } = null!;

        public bool? SwCohabitan { get; set; }

        // Propiedades de navegación
        public virtual Pariente Pariente { get; set; } = null!;
        public virtual Alumno Postulante { get; set; } = null!;
    }
}
