using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("SharingDayHorario", Schema = "inscripcion")]
    public class SharingDayHorario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdharingDayHorario { get; set; }

        [StringLength(100)]
        public string HoraInicio { get; set; } = null!;

        [StringLength(100)]
        public string HoraFin { get; set; } = null!;

        public bool? SwActivo { get; set; }

        public virtual ICollection<SharingDayTurno> SharingDayTurnos { get; set; } = new List<SharingDayTurno>();
    }
}
