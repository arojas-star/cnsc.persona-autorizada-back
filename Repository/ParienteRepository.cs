using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Dtos;
using personaAutorizadaBack.Repository.Interfaces;

namespace personaAutorizadaBack.Repository
{
    public class ParienteRepository : IParienteRepository
    {
        private readonly AppDbContext _context;
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public ParienteRepository(AppDbContext appDbContext, IDbContextFactory<AppDbContext> contextFactory)
        {
            _context = appDbContext;
            _contextFactory = contextFactory;
        }

        public async Task<PadreResumenDto?> ExistePadreXTipoNumeroDocumento(int TipoDocumento, string NumeroDocumento)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var tipoDocumentoParam = new SqlParameter("@CodigoTipoDocumento", System.Data.SqlDbType.BigInt) { Value = TipoDocumento };
            var NumeroDocumentoParam = new SqlParameter("@NumeroDocumento", System.Data.SqlDbType.VarChar) { Value = NumeroDocumento };
            var registros = await _context.Database.SqlQueryRaw<PadreResumenDto>($"EXEC sistemas.usp_Pariente_ObtenerXDocumento @CodigoTipoDocumento, @NumeroDocumento", tipoDocumentoParam, NumeroDocumentoParam).ToListAsync();

            return registros.FirstOrDefault();
        }
    }
}
