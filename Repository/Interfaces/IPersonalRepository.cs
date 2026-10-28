using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IPersonalRepository
    {
        Task<Personal?> GetPersonal(int CodigoPersonal);
        Task<Personal?> GetPersonalByDocument(int DocumentType, string DocumentNumber);
    }
}
