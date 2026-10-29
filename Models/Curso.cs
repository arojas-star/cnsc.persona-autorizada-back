using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Curso", Schema = "notas")]
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoCurso { get; set; }

        [StringLength(60)]
        public string NombreCurso { get; set; } = null!;

        [StringLength(10)]
        public string? NombreCorto { get; set; }

        public bool? SwConducta { get; set; }

        public bool? SwActivo { get; set; }

        [StringLength(3)]
        [Column(TypeName = "char(3)")]
        public string? CodCursoAnt { get; set; }

        public int? CodigoUsuario { get; set; }

        public DateTime? UltimaFecha { get; set; }

        public virtual ICollection<SharingDayTurno> SharingDayTurnos { get; set; } = new List<SharingDayTurno>();
    }
}
