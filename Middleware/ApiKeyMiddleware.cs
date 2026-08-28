namespace personaAutorizadaBack.Middleware
{
    public class ApiKeyMiddleware
    {
        private const string ApiKeyHeaderName = "X-API-KEY";

        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(
            RequestDelegate next,
            IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var configuredApiKey = _configuration["Security:ApiKey"];

            if (!context.Request.Headers.TryGetValue(
                    ApiKeyHeaderName,
                    out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "API Key requerida."
                });

                return;
            }

            if (!string.Equals(
                    extractedApiKey,
                    configuredApiKey,
                    StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "API Key inválida."
                });

                return;
            }

            await _next(context);
        }
    }
}
