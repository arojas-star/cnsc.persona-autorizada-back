using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("VessperienciasArea", Schema = "inscripcion")]
    public class VessperienciasArea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoArea { get; set; }

        [StringLength(50)]
        public string? DesArea { get; set; }

        // Propiedad de navegación hacia VessperienciasPersonal
        public virtual ICollection<VessperienciasPersonal> Personales { get; set; } = new List<VessperienciasPersonal>();
    }
}
