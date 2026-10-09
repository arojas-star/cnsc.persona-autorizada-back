using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("VessperienciasPersonal", Schema = "inscripcion")]
    public class VessperienciasPersonal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodigoPersonal { get; set; }

        [StringLength(250)]
        public string? ApeNom { get; set; }

        public int? CodigoArea { get; set; }

        // Propiedad de navegación
        [ForeignKey(nameof(CodigoArea))]
        public virtual VessperienciasArea? Area { get; set; }

        // Relación muchos a muchos a través de Vessperiencias
        public virtual ICollection<Vessperiencias> Vessperiencias { get; set; } = new List<Vessperiencias>();
    }
}
