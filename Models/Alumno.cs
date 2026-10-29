using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Alumno", Schema = "dbo")]
    public class Alumno
    {
        [Key]
        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string CodigoAlumno { get; set; } = null!;

        [StringLength(50)]
        public string? ApellidoPaterno { get; set; }

        [StringLength(50)]
        public string? ApellidoMaterno { get; set; }

        [StringLength(50)]
        public string? PrimerNombre { get; set; }

        [StringLength(50)]
        public string? SegundoNombre { get; set; }

        [StringLength(1)]
        [Column(TypeName = "char(1)")]
        public string? CodigoSexo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "char(20)")]
        public string? CodigoEducando { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoEstadoAlumno { get; set; }

        [StringLength(40)]
        [Column(TypeName = "char(40)")]
        public string? NombreArchivoFoto { get; set; }

        [StringLength(15)]
        [Column(TypeName = "char(15)")]
        public string? CodigoBanco { get; set; }

        public bool? SwExoneradoDocumentos { get; set; }
        public bool? SwExoneradoCuotaIngreso { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        [StringLength(6)]
        [Column(TypeName = "char(6)")]
        public string? UbigeoNacimiento { get; set; }

        public int? CodigoPaisNacimiento { get; set; }
        public int? CodigoColegioProcedencia { get; set; }

        [StringLength(255)]
        public string? DescripcionColegioProcedencia { get; set; }

        public int? CodigoReligion { get; set; }
        public bool? SwBautizo { get; set; }
        public int? CodigoIglesiaBautizo { get; set; }

        [StringLength(255)]
        public string? LugarBautizo { get; set; }

        public int? YearBautizo { get; set; }
        public DateTime? FechaBautizo { get; set; }
        public bool? SwPrimeraComunion { get; set; }
        public int? CodigoIglesiaPrimeraComunion { get; set; }

        [StringLength(255)]
        public string? LugarPrimeraComunion { get; set; }

        public int? YearPrimeraComunion { get; set; }
        public bool? SwConfirmacion { get; set; }
        public int? CodigoIglesiaConfirmacion { get; set; }

        [StringLength(255)]
        public string? LugarConfirmacion { get; set; }

        public int? YearConfirmacion { get; set; }
        public bool? SwActivo { get; set; }
        public int? CodigoUsuario { get; set; }
        public DateTime? UltimaFecha { get; set; }

        public virtual ICollection<CuadroFamiliar> CuadroFamiliares { get; set; } = new List<CuadroFamiliar>();
        public virtual ICollection<SharingDay> SharingDays { get; set; } = new List<SharingDay>();

        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
