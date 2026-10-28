namespace personaAutorizadaBack.Dtos
{
    public class EventoPapaResponse
    {
        public int CodigoPersonal { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        public string NumeroDocumento { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public int? Edad { get; set; }
    }

    public class EventoPapaHorarioResponse
    {
        public int codigoEvento { get; set; }
        public string Descripcion { get; set; } = string.Empty;

        public int CantidadCupos { get; set; }

        public int? MinEdad { get; set; }

        public int? MaxEdad { get; set; }

        public int Order { get; set; }
    }
}
