using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;

namespace personaAutorizadaBack.Repository
{
    public class EventoPapaRepository : IEventoPapaRepository
    {
        private readonly AppDbContext _context;

        public EventoPapaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventoPapaHorario>> GetSchedules()
        {
            return await _context.EventoPapaHorarios
                .Where(h =>
                    _context.EventosPapa
                    .Where(i => i.CodigoHorarioEventoPapa == h.CodigoHorarioEventoPapa)
                    .Count() != h.Vacantes
                )
                .ToListAsync();
        }

        public async Task<int> GetSpotsTake(int CodigoSchedule)
        {
            return await _context.EventosPapa
                .Where(e => 
                    e.CodigoHorarioEventoPapa == CodigoSchedule
                ).CountAsync();
        }

        public async Task<EventoPapaHorario?> GetSchedule(int CodigoSchedule)
        {
            return await _context.EventoPapaHorarios
                .Where(h =>
                    h.CodigoHorarioEventoPapa == CodigoSchedule
                )
                .FirstOrDefaultAsync();
        }

        public async Task<EventoPapa?> ExistInscription(int IdPersonal)
        {
            return await _context.EventosPapa.Where(e => e.CodigoPersonal == IdPersonal).FirstOrDefaultAsync();
        }

        public async Task RegisterInscripcion(EventoPapa dto)
        {
            await _context.EventosPapa.AddAsync(dto);

            await _context.SaveChangesAsync();
        }
    }
}
