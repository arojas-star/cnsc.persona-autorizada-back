using personaAutorizadaBack.Models;

namespace personaAutorizadaBack.Repository.Interfaces
{
    public interface IFeatureFlagRepository
    {
        Task<FeatureFlag?> GetAsync(string key);

        Task<bool> IsEnabledAsync(string key);
    }
}
