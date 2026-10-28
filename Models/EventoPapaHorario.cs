using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("EventoPapaHorario", Schema = "inscripcion")]
    public class EventoPapaHorario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoHorarioEventoPapa { get; set; }

        public string? Descripcion { get; set; }

        public int? Vacantes { get; set; }

        public int? MinEdad { get; set; }

        public int? MaxEdad { get; set; }

        // Propiedad de navegación
        public virtual ICollection<EventoPapa> EventosPapa { get; set; } = new List<EventoPapa>();
    }
}
