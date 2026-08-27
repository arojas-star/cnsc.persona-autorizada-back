namespace personaAutorizadaBack.Dtos
{
    public class InformacionAlumnDto
    {
        public AlumnoResumenDto AlumnoResumen { get; set; }

        public List<AlumnoResumenDto>? Parientes { get; set; }
    }
}
