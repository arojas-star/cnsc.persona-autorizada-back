using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Utils;

namespace personaAutorizadaBack.Repository
{
    public class FeatureFlagRepository : IFeatureFlagRepository
    {
        private readonly AppDbContext _context;

        public FeatureFlagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FeatureFlag?> GetAsync(string key)
        {
            return await _context.FeatureFlags
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdentificadorParametro == key);
        }

        public async Task<bool> IsEnabledAsync(string key)
        {
            var result = await _context.FeatureFlags
                .AsNoTracking()
                .Where(x => x.IdentificadorParametro == key)
                .Select(x => x.ValorParametro)
                .FirstOrDefaultAsync();

            return BoolUtil.ToBoolOrDefault(result, true);
        }
    }
}
