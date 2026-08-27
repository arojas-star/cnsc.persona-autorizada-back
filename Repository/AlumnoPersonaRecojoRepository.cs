using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;

namespace personaAutorizadaBack.Repository
{
    public class AlumnoPersonaRecojoRepository : IAlumnoPersonaRecojoRepository
    {
        private readonly AppDbContext _context;

        public AlumnoPersonaRecojoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlumnoPersonaRecojo>> ExisteRegistroXAlumno(string CodigoAlumno)
        {
            List<AlumnoPersonaRecojo> registros = await _context.AlumnoPersonasRecojo.Where(p => p.CodigoAlumno == CodigoAlumno).ToListAsync();

            return registros;
        }

        public async Task<AlumnoPersonaRecojo> RegistrarPersona(AlumnoPersonaRecojo alumnoRecojosDto)
        {
            var registro = new AlumnoPersonaRecojo
            {
                CodigoAlumno = alumnoRecojosDto.CodigoAlumno,
                NumeroDocumento = alumnoRecojosDto.NumeroDocumento,
                CodigoTipoDocumento = alumnoRecojosDto.CodigoTipoDocumento,
                ApellidoPaterno = alumnoRecojosDto.ApellidoPaterno,
                ApellidoMaterno = alumnoRecojosDto.ApellidoMaterno,
                Nombres = alumnoRecojosDto.Nombres,
                FechaEdicion = DateTime.Now,
                swActivo = true
            };

            await _context.AlumnoPersonasRecojo.AddAsync(registro);

            return registro;
        }

        public async Task RegistrarPersonasRecojo(List<AlumnoPersonaRecojo> alumnoRecojosDto)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var item in alumnoRecojosDto)
                {
                    await RegistrarPersona(item);
                }

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
