using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Salon", Schema = "dbo")]
    public class Salon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoSalon { get; set; }

        [StringLength(20)]
        [Column(TypeName = "char(20)")]
        public string? NombreSalon { get; set; }

        public int? CodigoYear { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char(1)")]
        public string? CodigoTurno { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoCentroCosto { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoNivel { get; set; }

        public byte? CodigoGrado { get; set; }

        public int? CodigoSeccion { get; set; }

        public int? CodigoPersonal { get; set; }

        public int? CodigoUsuario { get; set; }

        public DateTime? UltimaFecha { get; set; }

        public int? CodigoSalon_Ant { get; set; }

        public bool? SwCerrado { get; set; }

        public bool? SwActivo { get; set; }

        // Navegaciones
        public virtual Grado? Grado { get; set; }
        public virtual Seccion? Seccion { get; set; }
        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
