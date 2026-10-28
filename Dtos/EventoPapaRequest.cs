namespace personaAutorizadaBack.Dtos
{
    public class EventoPapaInscripcionRequest
    {
        public int CodigoEvento { get; set; }
        public int CodigoPersonal { get; set; }
        public string? Correo { get; set; } = String.Empty;
        public int? Edad { get; set; }
        public string? Celular { get; set; } = String.Empty;
    }

    public class EventoPapaPersonalRequest
    {
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
    }
}
