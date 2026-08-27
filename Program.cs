using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using personaAutorizadaBack.Configuration;
using personaAutorizadaBack.Repository;
using personaAutorizadaBack.Repository.Interfaces;
using personaAutorizadaBack.Services;
using personaAutorizadaBack.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var corsPolicyName = "AllowReactViteApp";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.Configure<ApiExternaSettings>(
    builder.Configuration.GetSection("ApiExterna"));

builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddScoped<IAlumnoPersonaRecojoRepository, AlumnoPersonaRecojoRepository>();
builder.Services.AddScoped<IAlumnoPersonaRecojoService, AlumnoPersonaRecojoService>();
builder.Services.AddScoped<IAlumnoService, AlumnoService>();

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

app.MapControllers();

app.Run();
