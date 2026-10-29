using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Matricula", Schema = "dbo")]
    public class Matricula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoMatricula { get; set; }

        public int? CodigoYear { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoCentroCosto { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char(1)")]
        public string? CodigoTurno { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoNivel { get; set; }

        public byte? CodigoGrado { get; set; }

        public int? CodigoSalon { get; set; }

        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string? CodigoAlumno { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoSituacionEvaluacion { get; set; }

        public int? CodigoEstadoMatricula { get; set; }

        public bool? SwActivo { get; set; }

        public DateTime? FechaMatricula { get; set; }

        // Navegaciones
        public virtual Salon? Salon { get; set; }
        public virtual Alumno? Alumno { get; set; }
        public virtual Grado? Grado { get; set; }
    }
}
