using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using personaAutorizadaBack.Models;
using personaAutorizadaBack.Options;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services.Interfaces;
using personaAutorizadaBack.Utils;

namespace personaAutorizadaBack.Services
{
    public class FeatureFlagService : IFeatureFlagService
    {
        private IFeatureFlagRepository _featureFlagRepository;
        private readonly IMemoryCache _cache;
        private readonly FeatureFlagOptions _options;

        private const string CachePrefix = "FEATURE_FLAG_";

        private static readonly SemaphoreSlim CacheLock = new(1, 1);

        public FeatureFlagService(
            IFeatureFlagRepository featureFlagRepository,
            IMemoryCache cache,
            IOptions<FeatureFlagOptions> options)
        {
            _featureFlagRepository = featureFlagRepository;
            _cache = cache;
            _options = options.Value;
        }

        public async Task<FeatureFlag?> GetAsync(string key, CancellationToken cancellationToken = default)
        {
            var cacheKey = GetCacheKey(key);

            // ---------------------------------------------
            // 1. Intentar obtener desde caché
            // ---------------------------------------------

            if (_cache.TryGetValue(
                cacheKey,
                out FeatureFlag? cached))
            {
                return cached;
            }

            // ---------------------------------------------
            // 2. Evitar múltiples consultas simultáneas
            // ---------------------------------------------

            await CacheLock.WaitAsync(
                cancellationToken);

            try
            {
                // -----------------------------------------
                // 3. Volver a revisar caché
                //
                // Otro request pudo haberlo cargado
                // mientras esperábamos el lock.
                // -----------------------------------------

                if (_cache.TryGetValue(
                    cacheKey,
                    out cached))
                {
                    return cached;
                }

                // -----------------------------------------
                // 4. Consultar BD
                // -----------------------------------------

                var featureFlag =
                    await _featureFlagRepository.GetAsync(key);

                // -----------------------------------------
                // 5. Guardar caché
                // -----------------------------------------

                var cacheDuration =
                    TimeSpan.FromSeconds(
                        _options.CacheDurationSeconds);

                _cache.Set(
                    cacheKey,
                    featureFlag,
                    cacheDuration);

                return featureFlag;
            }
            finally
            {
                CacheLock.Release();
            }
        }

        public async Task<bool> IsEnabledAsync(string key, CancellationToken cancellationToken = default)
        {
            var featureFlag = await GetAsync(key, cancellationToken);

            return BoolUtil.ToBoolOrDefault(featureFlag?.ValorParametro, true);
        }
        private static string GetCacheKey(string key)
        {
            return CachePrefix + key;
        }

    }
}
