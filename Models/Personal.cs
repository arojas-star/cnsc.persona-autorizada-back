using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("EventoPapaPersonal", Schema = "inscripcion")]
    public class Personal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoPersonal { get; set; }

        [StringLength(250)]
        [Column(TypeName = "varchar(250)")]
        public string? NombreCompleto { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? NumeroDocumento { get; set; }

        [StringLength(150)]
        [Column(TypeName = "varchar(150)")]
        public string? Correo { get; set; }

        public string? Celular { get; set; }

        // Propiedad de navegación
        public virtual ICollection<EventoPapa> EventosPapa { get; set; } = new List<EventoPapa>();
    }
}
