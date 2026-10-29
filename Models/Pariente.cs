using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace personaAutorizadaBack.Models
{
    [Table("Pariente", Schema = "admision")]
    public class Pariente
    {
        [Key]
        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string CodigoPariente { get; set; } = null!;

        [StringLength(50)]
        public string? ApellidoPaterno { get; set; }

        [StringLength(50)]
        public string? ApellidoMaterno { get; set; }

        [StringLength(50)]
        public string? PrimerNombre { get; set; }

        [StringLength(50)]
        public string? SegundoNombre { get; set; }

        public int? CodigoTipoPariente { get; set; }
        public bool? SwVive { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? CodigoPaisNacimiento { get; set; }
        public int? CodigoTipoDocumento { get; set; }

        [StringLength(50)]
        public string? NumeroDocumento { get; set; }

        [StringLength(50)]
        public string? CorreoPersonal { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoEstadoCivil { get; set; }

        [StringLength(50)]
        public string? NumeroTelefonoMovil { get; set; }

        public int? CodigoTipoVia { get; set; }

        [StringLength(255)]
        public string? NombreVia { get; set; }

        [StringLength(20)]
        public string? NumeroDomicilio { get; set; }

        [StringLength(20)]
        public string? ManzanaDomicilio { get; set; }

        [StringLength(100)]
        public string? InteriorDomicilio { get; set; }

        [StringLength(100)]
        public string? LoteDomicilio { get; set; }

        public int? CodigoTipoZona { get; set; }

        [StringLength(50)]
        public string? Urbanizacion { get; set; }

        [StringLength(255)]
        public string? ReferenciaDomicilio { get; set; }

        public int? CodigoPaisDomicilio { get; set; }

        [StringLength(6)]
        [Column(TypeName = "char(6)")]
        public string? UbigeoDomicilio { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoTipoVivienda { get; set; }

        [StringLength(50)]
        public string? NumeroTelefonoDomicilio { get; set; }

        [StringLength(2)]
        [Column(TypeName = "char(2)")]
        public string? CodigoTipoInstruccion { get; set; }

        public int? CodigoCentroInstruccion { get; set; }

        [StringLength(255)]
        public string? NombreCentroInstruccion { get; set; }

        public int? CodigoColegioEgreso { get; set; }

        [StringLength(255)]
        public string? DescripcionColegioEgreso { get; set; }

        public bool? ExAlumnoCarmelitas { get; set; }
        public int? AnoEgresoColegio { get; set; }
        public int? CodigoTipoProfesion { get; set; }

        [StringLength(255)]
        public string? CentrodeTrabajo { get; set; }

        public bool? SwTrabajadorCarmelitas { get; set; }
        public int? CodigoTipoSituacionLaboral { get; set; }

        [StringLength(255)]
        public string? DescripcionCargo { get; set; }

        [StringLength(255)]
        public string? DireccionTrabajo { get; set; }

        [StringLength(20)]
        public string? HorarioTrabajo { get; set; }

        public int? CodigoPaisTrabajo { get; set; }

        [StringLength(6)]
        [Column(TypeName = "char(6)")]
        public string? UbigeoTrabajo { get; set; }

        [StringLength(50)]
        public string? CorreoTrabajo { get; set; }

        [StringLength(50)]
        public string? TelefonoFijoTrabajo { get; set; }

        [StringLength(50)]
        public string? AnexoFijoTrabajo { get; set; }

        [StringLength(50)]
        public string? TelefonoMovilTrabajo { get; set; }

        public int? CodigoIdioma { get; set; }
        public int? CodigoReligion { get; set; }
        public bool? SwApoderado { get; set; }
        public bool? SwResponsableEconomico { get; set; }
        public bool? SwCorreoPersonalContacto { get; set; }
        public bool? SwCorreoTrabajoContacto { get; set; }
        public bool? SwActivo { get; set; }
        public bool? SwEstudioExtranjero { get; set; }

        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string? CodigoPariente2 { get; set; }

        [StringLength(8)]
        [Column(TypeName = "char(8)")]
        public string? CodigoAlumnoEgresado { get; set; }

        [StringLength(255)]
        public string? DireccionDomicilio { get; set; }

        public bool? SwHijoExtraMatrimonial { get; set; }

        public virtual ICollection<CuadroFamiliar> CuadroFamiliares { get; set; } = new List<CuadroFamiliar>();
        public virtual ICollection<SharingDay> SharingDays { get; set; } = new List<SharingDay>();
    }
}
