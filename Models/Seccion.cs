using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Seccion", Schema = "dbo")]
    public class Seccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoSeccion { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char(1)")]
        public string? Descripcion { get; set; }

        [StringLength(3)]
        [Column(TypeName = "char(3)")]
        public string? Codigo_Ant { get; set; }

        public bool? SwActivo { get; set; }

        public virtual ICollection<SharingDayTurno> SharingDayTurnos { get; set; } = new List<SharingDayTurno>();

        // Agregar a las propiedades de navegación de Seccion
        public virtual ICollection<Salon> Salones { get; set; } = new List<Salon>();
    }
}
