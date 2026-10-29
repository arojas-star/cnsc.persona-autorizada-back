using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("SharingDay", Schema = "inscripcion")]
    public class SharingDay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSharingDay { get; set; }

        public int IdSharingDayTurno { get; set; }

        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string CodigoAlumno { get; set; } = null!;

        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string CodigoPariente { get; set; } = null!;

        public DateTime? FechaRegistro { get; set; }

        // Propiedades de Navegación
        public virtual SharingDayTurno SharingDayTurno { get; set; } = null!;
        public virtual Pariente Pariente { get; set; } = null!;
        public virtual Alumno Alumno { get; set; } = null!;
    }
}
