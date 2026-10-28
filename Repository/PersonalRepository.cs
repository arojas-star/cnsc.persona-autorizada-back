using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;

namespace personaAutorizadaBack.Repository
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly AppDbContext _context;

        public PersonalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Personal?> GetPersonal(int CodigoPersonal)
        {
            return await _context.Personal.Where(p => p.CodigoPersonal == CodigoPersonal).FirstOrDefaultAsync();
        }

        public async Task<Personal?> GetPersonalByDocument(int DocumentType, string DocumentNumber)
        {
            return await _context.Personal.Where(p => p.NumeroDocumento == DocumentNumber).FirstOrDefaultAsync();
        }
    }
}
