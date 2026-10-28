using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Vessperiencias", Schema = "inscripcion")]
    public class Vessperiencias
    {
        [Key]
        [Column(Order = 0)]
        public int CodigoPersonal { get; set; }

        [Key]
        [Column(Order = 1)]
        public int CodigoPonencia { get; set; }

        public int CodigoTurno { get; set; }

        // Propiedades de navegación para las claves foráneas
        [ForeignKey(nameof(CodigoPersonal))]
        public virtual VessperienciasPersonal Personal { get; set; } = null!;

        [ForeignKey(nameof(CodigoPonencia))]
        public virtual VessperienciasPonencia Ponencia { get; set; } = null!;
    }
}
