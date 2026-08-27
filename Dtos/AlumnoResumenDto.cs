namespace personaAutorizadaBack.Dtos
{
    public class AlumnoResumenDto
    {
        public string CodigoAlumno { get; set; }
        public int TipoDocumentoId { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreCompleto { get; set; }
        public string Nivel { get; set; }
        public string Grado { get; set; }
        public string Seccion { get; set; }
    }
}
