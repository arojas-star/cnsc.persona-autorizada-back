using Microsoft.EntityFrameworkCore;
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
            return await _context.Database.SqlQuery<AlumnoResumenDto>($"""
                    select
                	    a.CodigoAlumno,
                        tp.CodigoTipoDocumento TipoDocumentoId,
                	    TRIM(tp.Descripcion) TipoDocumento,
                	    doc.NumeroDocumento,
                	    TRIM(CONCAT(UPPER(a.PrimerNombre), CONCAT(' ', CONCAT(UPPER(a.SegundoNombre), CONCAT(' ', CONCAT(UPPER(a.ApellidoPaterno), CONCAT(' ', UPPER(a.ApellidoMaterno)))))))) NombreCompleto,
                	    TRIM(n.DescripcionNivel) Nivel,
                	    TRIM(g.DescripcionGrado) Grado,
                	    TRIM(sec.Descripcion) Seccion
                    from Alumno a
                    inner join matricula.AlumnoxNumDocumento doc on doc.CodigoAlumno = a.CodigoAlumno
                    inner join matricula.TipoDocumento tp on tp.CodigoTipoDocumento = doc.CodigoTipoDocumento
                    inner join Matricula m on m.CodigoAlumno = a.CodigoAlumno
                    inner join Nivel n on n.CodigoNivel = m.CodigoNivel
                    inner join Grado g on g.CodigoGrado = m.CodigoGrado
                    inner join Salon s on s.CodigoSalon = m.CodigoSalon
                    inner join Seccion sec on sec.CodigoSeccion = s.CodigoSeccion
                    where a.SwActivo = 1
                	    and m.CodigoYear = {DateTime.Now.Year}
                	    and m.SwActivo = 1
                	    and a.CodigoAlumno = {codigoAlumno}
                """).FirstOrDefaultAsync();
        }

        public async Task<AlumnoResumenDto?> ExisteAlumnoPorTipoDocumentoNumeroDocumento(int TipoDocumento, string NumeroDocumento)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await _context.Database.SqlQuery<AlumnoResumenDto>($"""
                   select
                	    a.CodigoAlumno,
                        tp.CodigoTipoDocumento TipoDocumentoId,
                	    TRIM(tp.Descripcion) TipoDocumento,
                	    doc.NumeroDocumento,
                	    TRIM(CONCAT(UPPER(a.PrimerNombre), CONCAT(' ', CONCAT(UPPER(a.SegundoNombre), CONCAT(' ', CONCAT(UPPER(a.ApellidoPaterno), CONCAT(' ', UPPER(a.ApellidoMaterno)))))))) NombreCompleto,
                	    TRIM(n.DescripcionNivel) Nivel,
                	    TRIM(g.DescripcionGrado) Grado,
                	    TRIM(sec.Descripcion) Seccion
                    from Alumno a
                    inner join matricula.AlumnoxNumDocumento doc on doc.CodigoAlumno = a.CodigoAlumno
                    inner join matricula.TipoDocumento tp on tp.CodigoTipoDocumento = doc.CodigoTipoDocumento
                    inner join Matricula m on m.CodigoAlumno = a.CodigoAlumno
                    inner join Nivel n on n.CodigoNivel = m.CodigoNivel
                    inner join Grado g on g.CodigoGrado = m.CodigoGrado
                    inner join Salon s on s.CodigoSalon = m.CodigoSalon
                    inner join Seccion sec on sec.CodigoSeccion = s.CodigoSeccion
                    where a.SwActivo = 1
                	    and m.CodigoYear = 2026
                	    and m.SwActivo = 1
                	    and doc.NumeroDocumento = {NumeroDocumento}
                	    and doc.CodigoTipoDocumento = {TipoDocumento}
                """).FirstOrDefaultAsync();
        }

        public async Task<List<AlumnoResumenDto>> ObtenerHermanosAlumnos(string CodigoAlumno)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await _context.Database.SqlQuery<AlumnoResumenDto>($"""
                    select
                	    a.CodigoAlumno,
                        tp.CodigoTipoDocumento TipoDocumentoId,
                	    TRIM(tp.Descripcion) TipoDocumento,
                	    doc.NumeroDocumento,
                	    TRIM(CONCAT(UPPER(a.PrimerNombre), CONCAT(' ', CONCAT(UPPER(a.SegundoNombre), CONCAT(' ', CONCAT(UPPER(a.ApellidoPaterno), CONCAT(' ', UPPER(a.ApellidoMaterno)))))))) NombreCompleto,
                	    TRIM(n.DescripcionNivel) Nivel,
                	    TRIM(g.DescripcionGrado) Grado,
                	    TRIM(sec.Descripcion) Seccion
                    from CuadroFamiliar cf
                        inner join CuadroFamiliar f on f.CodigoFamilia = cf.CodigoFamilia and cf.CodigoAlumno <> f.CodigoAlumno and f.swResEco = 1
                        inner join Alumno a on a.CodigoAlumno = f.CodigoAlumno
                        inner join matricula.AlumnoxNumDocumento doc on doc.CodigoAlumno = a.CodigoAlumno
                        inner join matricula.TipoDocumento tp on tp.CodigoTipoDocumento = doc.CodigoTipoDocumento
                        inner join Matricula m on m.CodigoAlumno = a.CodigoAlumno
                        inner join Nivel n on n.CodigoNivel = m.CodigoNivel
                        inner join Grado g on g.CodigoGrado = m.CodigoGrado
                        inner join Salon s on s.CodigoSalon = m.CodigoSalon
                        inner join Seccion sec on sec.CodigoSeccion = s.CodigoSeccion
                    where cf.CodigoAlumno = {CodigoAlumno}
                    GROUP BY a.CodigoAlumno, tp.Descripcion, doc.NumeroDocumento,
                    a.PrimerNombre, a.SegundoNombre, a.ApellidoPaterno, a.ApellidoMaterno,
                    n.DescripcionNivel, g.DescripcionGrado, sec.Descripcion, tp.CodigoTipoDocumento
                """).ToListAsync();
        }

        public async Task<List<TipoDocumentoResumenDto>> ObtenerListaTiposDocumentos()
        {
            return await _context.Database.SqlQuery<TipoDocumentoResumenDto>($"""
                select CodigoTipoDocumento, Descripcion from matricula.TipoDocumento where SwActivo = 1 and Tipo = 'Identidad';
                """).ToListAsync();
        }
    }
}
