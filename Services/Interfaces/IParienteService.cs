using personaAutorizadaBack.Dtos;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IParienteService
    {
        Task<PadreResumenDto?> ObtenerParienteXTipoNumeroDocumento(int CodigoTipoDocumento, string NumeroDocumento);
    }
}
