using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IPersonalService
    {
        Task<EventoPapaResponse?> GetPersonal(int CodigoPersonal);
        Task<EventoPapaResponse?> GetPersonalByDocument(EventoPapaPersonalRequest dto);
        Task UpdatePersonal(int CodigoPersonal, string Correo, string Celular);
    }
}
