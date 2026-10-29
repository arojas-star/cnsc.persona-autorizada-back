namespace personaAutorizadaBack.Dtos
{
    public class PadreSharingDayResponse
    {
        public string NombreCompleto { get; set; } = String.Empty;

        public string NumeroDocumento { get; set; } = String.Empty;

        public string CodigoPariente { get; set; } = String.Empty;

        public string CodigoAlumno { get; set; } = String.Empty;

        public string NombreAlumno { get; set; } = String.Empty;
    }

    public class TurnoSharingDayResponse
    {
        public int CodigoTurno { get; set; }

        public string Curso { get; set; } = String.Empty;

        public string HoraInicio { get; set; } = String.Empty;

        public string HoraFin { get; set; } = String.Empty;
    }

    public class TurnoSharingDayData
    {
        public int CodigoTurno { get; set; }

        public int Vacante { get; set; }

        public int Tomados { get; set; }
    }
}
