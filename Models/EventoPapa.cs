using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("EventoPapa", Schema = "inscripcion")]
    public class EventoPapa
    {
        public int CodigoPersonal { get; set; }

        public int CodigoHorarioEventoPapa { get; set; }

        public DateTime? FechaRegistro { get; set; }

        // Propiedades de navegación para las relaciones
        public virtual Personal Personal { get; set; } = null!;
        public virtual EventoPapaHorario EventoPapaHorario { get; set; } = null!;
    }
}
