using personaAutorizadaBack.Dtos.ApiExterna;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IApiExternaService
    {
        Task<ClienteResponse?> ObtenerInfoPersona(string NumeroDocumento);
    }
}
