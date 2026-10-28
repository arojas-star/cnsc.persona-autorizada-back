using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IPersonalRepository
    {
        Task<Personal?> GetPersonal(int CodigoPersonal);
        Task<Personal?> GetPersonalByDocument(int DocumentType, string DocumentNumber);
        Task UpdateCorreo(int CodigoPersonal, string Correo);
        Task UpdateCelular(int CodigoPersonal, string Celular);
    }
}
