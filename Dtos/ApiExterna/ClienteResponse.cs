using System.Text.Json.Serialization;

namespace personaAutorizadaBack.Dtos.ApiExterna
{
    public class Data
    {
        [JsonPropertyName("dni")]
        public string Dni { get; set; } = string.Empty;

        [JsonPropertyName("nombres")]
        public string Nombres { get; set; } = string.Empty;

        [JsonPropertyName("apellido_paterno")]
        public string ApellidoPaterno { get; set; } = string.Empty;

        [JsonPropertyName("apellido_materno")]
        public string ApellidoMaterno { get; set; } = string.Empty;

        [JsonPropertyName("fecha_nacimiento")]
        public string FechaNacimiento { get; set; } = string.Empty;

        [JsonPropertyName("sexo")]
        public string Sexo { get; set; } = string.Empty;

        [JsonPropertyName("direccion")]
        public string Direccion { get; set; } = string.Empty;

        [JsonPropertyName("ubigeo")]
        public string Ubigeo { get; set; } = string.Empty;

        [JsonPropertyName("distrito")]
        public string Distrito { get; set; } = string.Empty;

        [JsonPropertyName("provincia")]
        public string Provincia { get; set; } = string.Empty;

        [JsonPropertyName("departamento")]
        public string Departamento { get; set; } = string.Empty;

        [JsonPropertyName("estado_civil")]
        public string EstadoCivil { get; set; } = string.Empty;

        [JsonPropertyName("restriccion")]
        public string Restriccion { get; set; } = string.Empty;

        [JsonPropertyName("foto")]
        public string Foto { get; set; } = string.Empty;
    }
    public class ClienteResponse
    {
        public string Respuesta { get; set; } = string.Empty;

        public Data? Data { get; set; }

        public int Codigo { get; set; }
    }
}
