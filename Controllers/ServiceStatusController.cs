using Microsoft.AspNetCore.Mvc;
using personaAutorizadaBack.Services.Interfaces;
using personaAutorizadaBack.Utils;

[ApiController]
[Route("[controller]")]
public class ServiceStatusController : ControllerBase
{
    private readonly IFeatureFlagService _featureFlagService;

    public ServiceStatusController(
        IFeatureFlagService featureFlagService)
    {
        _featureFlagService = featureFlagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStatus()
    {
        var featureFlag =
            await _featureFlagService.GetAsync("API_GLOBAL");

        if (featureFlag == null)
        {
            return Ok(new
            {
                enabled = true
            });
        }

        return Ok(new
        {
            enabled = BoolUtil.ToBoolOrDefault(featureFlag.ValorParametro, true),
            message = featureFlag.DescripcionParametro,
            contactEmail = "administracion@carmelitas.edu.pe"
        });
    }
}