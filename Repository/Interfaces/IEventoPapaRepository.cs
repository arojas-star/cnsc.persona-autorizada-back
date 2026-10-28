using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IEventoPapaRepository
    {
        Task<List<EventoPapaHorario>> GetSchedules();

        Task<EventoPapaHorario?> GetSchedule(int CodigoSchedule);

        Task<EventoPapa?> ExistInscription(int IdPersonal);

        Task RegisterInscripcion(EventoPapa dto);

        Task<int> GetSpotsTake(int CodigoSchedule);
    }
}
