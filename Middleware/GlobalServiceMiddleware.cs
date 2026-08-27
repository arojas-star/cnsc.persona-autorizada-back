using personaAutorizadaBack.Services.Interfaces;
using personaAutorizadaBack.Utils;

namespace personaAutorizadaBack.Middleware
{
    public class GlobalServiceMiddleware
    {
        private readonly RequestDelegate _next;

        private const string GlobalServiceKey = "PER_AUT";

        public GlobalServiceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            IFeatureFlagService featureFlagService)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Endpoints que siempre deben estar disponibles
            if (IsExcludedPath(path))
            {
                await _next(context);
                return;
            }

            var featureFlag = await featureFlagService.GetAsync(GlobalServiceKey, context.RequestAborted);

            // Si no existe el flag, por seguridad
            // puedes decidir si permitir o bloquear.
            if (featureFlag == null)
            {
                await _next(context);
                return;
            }

            if (!BoolUtil.ToBoolOrDefault(featureFlag.ValorParametro, true))
            {
                context.Response.StatusCode =
                    StatusCodes.Status503ServiceUnavailable;

                context.Response.ContentType = "application/json";

                var response = new
                {
                    code = "SERVICE_DISABLED",
                    message = string.IsNullOrWhiteSpace(featureFlag.DescripcionParametro)
                        ? "El servicio se encuentra temporalmente fuera de servicio."
                        : featureFlag.DescripcionParametro,
                    contactEmail = "administracion@carmelitas.edu.pe"
                };

                await context.Response.WriteAsJsonAsync(response);

                return;
            }

            await _next(context);
        }

        private static bool IsExcludedPath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            return path.StartsWith("/swagger")
                || path.StartsWith("/health")
                || path.StartsWith("/ServiceStatus");
        }
    }
}
