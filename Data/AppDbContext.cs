using Microsoft.EntityFrameworkCore;
using personaAutorizadaBack.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AlumnoPersonaRecojo> AlumnoPersonasRecojo => Set<AlumnoPersonaRecojo>();
    public DbSet<FeatureFlag> FeatureFlags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AlumnoPersonaRecojo>()
            .HasKey(x => new
            {
                x.CodigoAlumno,
                x.NumeroDocumento,
            });

        modelBuilder.Entity<AlumnoPersonaRecojo>()
            .ToTable("AlumnoPersonaRecojo", schema: "matricula");

        modelBuilder.Entity<FeatureFlag>(entity =>
        {
            entity.ToTable("Parametro", schema: "sistemas");

            entity.HasKey(x => x.IdentificadorParametro);
        });
    }
}
