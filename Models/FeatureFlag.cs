namespace personaAutorizadaBack.Models
{
    public class FeatureFlag
    {
        public string IdentificadorParametro { get; set; }

        public string DescripcionParametro { get; set; } = string.Empty;

        public string ValorParametro { get; set; }
    }
}
