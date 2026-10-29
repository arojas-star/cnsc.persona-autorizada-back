using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using personaAutorizadaBack.Configuration;
using personaAutorizadaBack.Middleware;
using personaAutorizadaBack.Options;
using personaAutorizadaBack.Repository;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services;
using personaAutorizadaBack.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins =
    builder.Configuration
        .GetSection("Cors:AllowedOrigins")
        .Get<string[]>() ?? Array.Empty<string>();

var corsPolicyName = "AllowReactViteApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins(
                allowedOrigins
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.Configure<ApiKeyOptions>(builder.Configuration.GetSection("ApiKeyOptions"));


builder.Services.AddMemoryCache();

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseCompatibilityLevel(120)
    ));

builder.Services.Configure<ApiExternaSettings>(
    builder.Configuration.GetSection("ApiExterna"));

builder.Services.Configure<FeatureFlagOptions>(
    builder.Configuration.GetSection("FeatureFlags"));

builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IAlumnoPersonaRecojoRepository, AlumnoPersonaRecojoRepository>();
builder.Services.AddScoped<IFeatureFlagRepository, FeatureFlagRepository>();
builder.Services.AddScoped<IParienteRepository, ParienteRepository>();
builder.Services.AddScoped<IVessperienciasRepository, VessperienciasRepository>();
builder.Services.AddScoped<IEventoPapaRepository, EventoPapaRepository>();
builder.Services.AddScoped<IPersonalRepository, PersonalRepository>();

builder.Services.AddScoped<IAlumnoPersonaRecojoService, AlumnoPersonaRecojoService>();
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IFeatureFlagService, FeatureFlagService>();
builder.Services.AddScoped<IParienteService, ParienteService>();
builder.Services.AddScoped<IVessperienciasServices, VessperienciasServices>();
builder.Services.AddScoped<IPersonalService, PersonalService>();
builder.Services.AddScoped<IEventoPapaService, EventoPapaService>();

builder.Services.AddScoped<ISharingDayService, SharingDayService>();

builder.Services.AddHttpClient<IApiExternaService, ApiExternaService>(
    (serviceProvider, client) =>
    {
        var settings = serviceProvider
            .GetRequiredService<IOptions<ApiExternaSettings>>()
            .Value;

        if (string.IsNullOrWhiteSpace(settings.BaseUrl))
        {
            throw new InvalidOperationException(
                "ApiExterna:BaseUrl no está configurada");
        }

        if (string.IsNullOrWhiteSpace(settings.ApiKey))
        {
            throw new InvalidOperationException(
                "ApiExterna:ApiKey no está configurada");
        }

        client.BaseAddress = new Uri(settings.BaseUrl);

        client.DefaultRequestHeaders.Add(
            "Authorization",
            $"Bearer {settings.ApiKey}");
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(corsPolicyName);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseMiddleware<GlobalServiceMiddleware>();


app.MapControllers();

app.Run();
