using personaAutorizadaBack.Dtos;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IParienteRepository
    {
        Task<PadreResumenDto?> ExistePadreXTipoNumeroDocumento(int TipoDocumento, string NumeroDocumento);
    }
}
