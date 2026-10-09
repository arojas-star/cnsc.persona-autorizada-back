using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("VessperienciasPonencia", Schema = "inscripcion")]
    public class VessperienciasPonencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoPonencia { get; set; }

        public string? Tema { get; set; }

        public int? Vacantes { get; set; }

        // Relación muchos a muchos a través de Vessperiencias
        public virtual ICollection<Vessperiencias> Vessperiencias { get; set; } = new List<Vessperiencias>();
    }
}
