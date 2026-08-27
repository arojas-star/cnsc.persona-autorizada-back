namespace personaAutorizadaBack.Models
{
    public class AlumnoPersonaRecojo
    {
        public string CodigoAlumno { get; set; }
        public string NumeroDocumento { get; set; }
        public int CodigoTipoDocumento { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public bool swActivo { get; set; }
        public DateTime FechaEdicion { get; set; }
    }
}
