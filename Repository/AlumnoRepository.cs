using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Repository.Interfaces;

namespace personaAutorizadaBack.Repository
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public AlumnoRepository(AppDbContext appDbContext, IDbContextFactory<AppDbContext> contextFactory)
        {
            _context = appDbContext;
            _contextFactory = contextFactory;
        }

        public async Task<AlumnoResumenDto?> ExisteAlumnoPorCodigo(string codigoAlumno)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var registros = await _context.Database.SqlQuery<AlumnoResumenDto>($"EXEC sistemas.usp_Alumnos_ObtenerActivoXDocumentoCodigo @CodigoAlumno = {codigoAlumno};").ToListAsync();

            return registros.FirstOrDefault();
        }

        public async Task<AlumnoResumenDto?> ExisteAlumnoPorTipoDocumentoNumeroDocumento(int TipoDocumento, string NumeroDocumento)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            var registros = await _context.Database.SqlQuery<AlumnoResumenDto>($"EXEC sistemas.usp_Alumnos_ObtenerActivoXDocumentoCodigo {NumeroDocumento}, {TipoDocumento};").ToListAsync();

            return registros.FirstOrDefault();
        }

        public async Task<List<AlumnoResumenDto>> ObtenerHermanosAlumnos(string CodigoAlumno)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await _context.Database.SqlQuery<AlumnoResumenDto>($"EXEC sistemas.usp_Alumnos_ObtenerHermanos @CodigoAlumno = {CodigoAlumno};").ToListAsync();
        }

        public async Task<List<TipoDocumentoResumenDto>> ObtenerListaTiposDocumentos()
        {
            return await _context.Database.SqlQuery<TipoDocumentoResumenDto>($"EXEC matricula.usp_TipoDocumento_Listar;").ToListAsync();
        }
    }
}
