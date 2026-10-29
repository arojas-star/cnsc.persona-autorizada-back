using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Grado", Schema = "dbo")]
    public class Grado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public byte CodigoGrado { get; set; }

        [StringLength(20)]
        public string DescripcionGrado { get; set; } = null!;

        [StringLength(10)]
        public string? NombreCorto { get; set; }

        public bool? SwActivo { get; set; }

        public virtual ICollection<SharingDayTurno> SharingDayTurnos { get; set; } = new List<SharingDayTurno>();

        // Agregar a las propiedades de navegación de Grado
        public virtual ICollection<Salon> Salones { get; set; } = new List<Salon>();
        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
