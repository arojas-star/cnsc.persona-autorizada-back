using personaAutorizadaBack.Dtos;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IEventoPapaService
    {
        Task<List<EventoPapaHorarioResponse>> GetHorarios();

        Task<(bool?, string)> RegisterInscription(EventoPapaInscripcionRequest dto);
    }
}
