using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Services.Interfaces;

namespace personaAutorizadaBack.Services
{
    public class SharingDayService : ISharingDayService
    {
        private readonly AppDbContext _context;

        public SharingDayService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool, PadreSharingDayResponse?)> FindParent(ParentSharingDayRequest dto)
        {
            int ActualYear = DateTime.Now.Year;

            var parent = await (
                from p in _context.Parientes
                join cf in _context.CuadroFamiliares
                    on p.CodigoPariente equals cf.CodigoPariente
                join a in _context.Alumnos
                    on cf.CodigoPostulante equals a.CodigoAlumno
                join m in _context.Matriculas
                    on a.CodigoAlumno equals m.CodigoAlumno
                join s in _context.Salones
                    on m.CodigoSalon equals s.CodigoSalon
                where m.CodigoYear == ActualYear & m.SwActivo == true &&
                    p.NumeroDocumento!.Trim() == dto.NumeroDocumento.Trim() && p.CodigoTipoDocumento == dto.CodigoTipoDocumento &&
                    m.CodigoGrado == dto.CodigoGrado && s.CodigoSeccion == dto.CodigoSeccion
                select new PadreSharingDayResponse
                {
                    CodigoAlumno = a.CodigoAlumno,
                    CodigoPariente = p.CodigoPariente,
                    NombreCompleto = $"{p.PrimerNombre} {p.ApellidoPaterno} {p.ApellidoMaterno}".Trim(),
                    NumeroDocumento = p.NumeroDocumento!.Trim(),
                    NombreAlumno = $"{a.PrimerNombre} {a.ApellidoPaterno}".Trim()
                }
            ).FirstOrDefaultAsync();

            return ( parent != null , parent );
        }

        public async Task<PadreSharingDayResponse?> GetParent(string CodigoPariente)
        {
            int ActualYear = DateTime.Now.Year;

            var parent = await (
                from p in _context.Parientes
                join cf in _context.CuadroFamiliares
                    on p.CodigoPariente equals cf.CodigoPariente
                join a in _context.Alumnos
                    on cf.CodigoPostulante equals a.CodigoAlumno
                join m in _context.Matriculas
                    on a.CodigoAlumno equals m.CodigoAlumno
                join s in _context.Salones
                    on m.CodigoSalon equals s.CodigoSalon
                where m.CodigoYear == ActualYear & m.SwActivo == true &&
                    p.CodigoPariente == CodigoPariente
                select new PadreSharingDayResponse
                {
                    CodigoAlumno = a.CodigoAlumno,
                    CodigoPariente = p.CodigoPariente,
                    NombreCompleto = $"{p.PrimerNombre} {p.ApellidoPaterno} {p.ApellidoMaterno}".Trim(),
                    NumeroDocumento = p.NumeroDocumento!.Trim(),
                    NombreAlumno = $"{a.PrimerNombre} {a.ApellidoPaterno}".Trim()
                }
            ).FirstOrDefaultAsync();

            return parent;
        }

        public async Task<TurnoSharingDayData?> GetTurnsTotalSpotsTake(int idTurn)
        {
            var total = await _context.SharingDays.Where(i => i.IdSharingDayTurno == idTurn).CountAsync();

            return await _context.SharingDayTurnos.Where(t => t.IdSharingDayTurno == idTurn).Select(i => new TurnoSharingDayData
            {
                CodigoTurno = i.IdSharingDayTurno,
                Tomados = total,
                Vacante = i.Vacantes
            }).FirstOrDefaultAsync();
        }

        public async Task<List<TurnoSharingDayResponse>> GetTurns(int CodigoGrado, int CodigoSeccion)
        {
            var turns = await (
                from sdt in _context.SharingDayTurnos
                join sdh in _context.SharingDayHorarios
                    on sdt.IdharingDayHorario equals sdh.IdharingDayHorario
                join c in _context.Cursos
                    on sdt.CodigoCurso equals c.CodigoCurso
                join g in _context.Grados
                    on sdt.CodigoGrado equals g.CodigoGrado
                join s in _context.Secciones
                    on sdt.CodigoSeccion equals s.CodigoSeccion
                where sdt.SwActivo == true && sdh.SwActivo == true &&
                    sdt.CodigoGrado == CodigoGrado && sdt.CodigoSeccion == CodigoSeccion &&
                    sdt.Vacantes > _context.SharingDays.Where(e => e.IdSharingDayTurno == sdt.IdSharingDayTurno).Count()
                select new TurnoSharingDayResponse
                {
                    CodigoTurno = sdt.IdSharingDayTurno,
                    Curso = c.NombreCurso.Trim(),
                    HoraInicio = sdh.HoraInicio.Trim(),
                    HoraFin = sdh.HoraFin.Trim(),
                }
            ).ToListAsync();

            return turns;
        }

        public async Task<(bool?, string)> RegisterParent(RegisterSharingDayRequest dto)
        {
            var parent = await GetParent(dto.CodigoPariente);

            if (parent == null)
            {
                return (null, "No se encontró un Padre de Familia válido para registrar.");
            }

            var turn = await GetTurnsTotalSpotsTake(dto.CodigoTurno);

            if (turn == null)
            {
                return (null, "No se encontró un turno válido para registrar.");
            }

            var existePadreAlumno = await _context.SharingDays.Where(r => r.CodigoAlumno == parent.CodigoAlumno).FirstOrDefaultAsync();

            if (existePadreAlumno != null)
            {
                return (false, $"Ya se ha registrado un Padre de Familia para el alumno {parent.NombreAlumno}.");
            }

            if (turn.Vacante <= turn.Tomados)
            {
                return (false, "No hay cupos disponibles para poder registrarse.");
            }

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var inscripcion = new SharingDay
                {
                    CodigoAlumno = parent.CodigoAlumno,
                    CodigoPariente = parent.CodigoPariente,
                    FechaRegistro = DateTime.Now,
                    IdSharingDayTurno = turn.CodigoTurno
                };

                await _context.SharingDays.AddAsync(inscripcion);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return (true, "Correcto");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, ex.Message);
            }
        }
    }
}
