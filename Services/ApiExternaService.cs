using personaAutorizadaBack.Dtos.ApiExterna;
using personaAutorizadaBack.Services.Interfaces;
using System.Net;

namespace personaAutorizadaBack.Services
{
    public class ApiExternaService : IApiExternaService
    {
        private readonly HttpClient _httpClient;

        public ApiExternaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClienteResponse?> ObtenerInfoPersona(string NumeroDocumento)
        {
            var response = await _httpClient.GetAsync(
                $"/api/v2/dni/{Uri.EscapeDataString(NumeroDocumento)}"
            );

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            // Otro error HTTP
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"La API externa respondió con HTTP {(int)response.StatusCode}");
            }

            var resultado = await response.Content
                .ReadFromJsonAsync<ClienteResponse>();

            if (resultado?.Data is null)
            {
                return null;
            }

            // Si necesitas considerar que no tiene datos
            if (string.IsNullOrWhiteSpace(resultado.Data.Dni))
            {
                return null;
            }


            return resultado;
        }
    }
}
