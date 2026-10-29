using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Collections.Specialized.BitVector32;

namespace personaAutorizadaBack.Models
{
    [Table("SharingDayTurno", Schema = "inscripcion")]
    public class SharingDayTurno
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSharingDayTurno { get; set; }

        public int IdharingDayHorario { get; set; }

        public byte CodigoGrado { get; set; }

        public int CodigoSeccion { get; set; }

        public int CodigoCurso { get; set; }

        public int Vacantes { get; set; } // Campo NOT NULL DEFAULT 0 en DDL

        public bool? SwActivo { get; set; }

        // Propiedades de Navegación
        public virtual SharingDayHorario SharingDayHorario { get; set; } = null!;
        public virtual Curso Curso { get; set; } = null!;
        public virtual Grado Grado { get; set; } = null!;
        public virtual Seccion Seccion { get; set; } = null!;

        public virtual ICollection<SharingDay> SharingDays { get; set; } = new List<SharingDay>();
    }
}
