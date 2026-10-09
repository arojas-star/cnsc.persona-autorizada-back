using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;

namespace personaAutorizadaBack.Repository
{
    public class VessperienciasRepository: IVessperienciasRepository
    {
        private readonly AppDbContext _context;

        public VessperienciasRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VessperienciasAreaResponse>> GetAreas()
        {
            return await _context.VessperienciasAreas.AsNoTracking().OrderBy(x => x.CodigoArea).Select(x => new VessperienciasAreaResponse(
                x.CodigoArea,
                x.DesArea
            )).ToListAsync();
        }

        public async Task<List<VessperienciasPersonalResponse>> GetPersonalXArea(int CodigoArea)
        {
            return await _context.VessperienciasPersonales.Where(x => x.CodigoArea == CodigoArea && 2 > _context.Vessperiencias.Where(i => i.CodigoPersonal == x.CodigoPersonal).Count() ).OrderBy(x => x.ApeNom).Select(x => new VessperienciasPersonalResponse
            (
                x.CodigoPersonal,
                x.ApeNom,
                x.CodigoArea
            )).ToListAsync();
        }

        public async Task<VessperienciasPersonal?> GetPersonal(int CodigoPersonal)
        {
            return await _context.VessperienciasPersonales.Where(p => p.CodigoPersonal == CodigoPersonal).FirstOrDefaultAsync();
        }

        public async Task<List<VessperienciasTemaResponse>> GetPonencias()
        {
            return await _context.VessperienciasPonencias
                .Where(x => 
                    x.Vacantes >  _context.Vessperiencias.Where(v => v.CodigoPonencia == x.CodigoPonencia).Count()
                ).OrderBy(x => x.Tema)
                .Select(x => new VessperienciasTemaResponse
                (
                    x.CodigoPonencia,
                    x.Tema
                )).ToListAsync();
        }

        public async Task<List<VessperienciasPonencia>> ValidatePonencias(List<int> CodigosPonencia)
        {
            return await _context.VessperienciasPonencias
                .Where(x =>
                    CodigosPonencia.Contains(x.CodigoPonencia) &&
                    x.Vacantes > _context.Vessperiencias.Where(v => v.CodigoPonencia == x.CodigoPonencia).Count()
                ).ToListAsync();
        }

        public async Task<string> GetTema(List<int> CodigosPonencia)
        {
            var results = await _context.VessperienciasPonencias
                .Where(x =>
                    CodigosPonencia.Contains(x.CodigoPonencia)
                ).ToListAsync();

            return string.Join(",", results.Select(x => x.Tema));
        }

        public async Task GuardarInscripcion(VessperienciasDto dto)
        {
            using var transaction = _context.Database.BeginTransaction();

            var data = dto.temas.Select(t => new Vessperiencias
            {
                CodigoPersonal = dto.codigoPersonal,
                CodigoPonencia = t
            }).ToList();

            try
            {
                await _context.Vessperiencias.AddRangeAsync(data);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            } catch
            {
                await transaction.RollbackAsync();
            }
        }
    }
}
