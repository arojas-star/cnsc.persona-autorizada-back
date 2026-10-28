namespace personaAutorizadaBack.Dtos
{
    public class EventoPapaInscripcionRequest
    {
        public int CodigoEvento { get; set; }
        public int CodigoPersonal { get; set; }
    }

    public class EventoPapaPersonalRequest
    {
        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; } = string.Empty;
    }
}
