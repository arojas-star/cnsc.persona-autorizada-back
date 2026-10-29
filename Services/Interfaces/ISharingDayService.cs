using personaAutorizadaBack.Dtos;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface ISharingDayService
    {
        Task<(bool, PadreSharingDayResponse?)> FindParent(ParentSharingDayRequest dto);

        Task<PadreSharingDayResponse?> GetParent(string CodigoPariente);

        Task<TurnoSharingDayData?> GetTurnsTotalSpotsTake(int idTurn);

        Task<List<TurnoSharingDayResponse>> GetTurns(int CodigoGrado, int CodigoSeccion);

        Task<(bool?, string)> RegisterParent(RegisterSharingDayRequest dto);
    }
}
