using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Services.Interfaces
{
    public interface IFeatureFlagService
    {
        Task<FeatureFlag?> GetAsync(string key, CancellationToken cancellationToken = default);

        Task<bool> IsEnabledAsync(string key, CancellationToken cancellationToken = default);
    }
}
