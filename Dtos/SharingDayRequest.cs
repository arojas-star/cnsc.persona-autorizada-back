namespace personaAutorizadaBack.Dtos
{
    public class ParentSharingDayRequest
    {
        public int CodigoTipoDocumento { get; set; }

        public string NumeroDocumento { get; set; } = String.Empty;

        public int CodigoGrado { get; set; }

        public int CodigoSeccion { get; set; }
    }

    public class RegisterSharingDayRequest
    {
        public string CodigoPariente { get; set; } = String.Empty;

        public int CodigoTurno { get; set; }
    }
}
