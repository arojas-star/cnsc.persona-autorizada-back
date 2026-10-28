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

    public DbSet<VessperienciasArea> VessperienciasAreas { get; set; }
    public DbSet<VessperienciasPersonal> VessperienciasPersonales { get; set; }
    public DbSet<VessperienciasPonencia> VessperienciasPonencias { get; set; }
    public DbSet<Vessperiencias> Vessperiencias { get; set; }

    public DbSet<Personal> Personal { get; set; }

    public DbSet<EventoPapaHorario> EventoPapaHorarios { get; set; } = null!;
    public DbSet<EventoPapa> EventosPapa { get; set; } = null!;

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

        // ==========================================
        // Configuración: VessperienciasArea
        // ==========================================
        modelBuilder.Entity<VessperienciasArea>(entity =>
        {
            entity.ToTable("VessperienciasArea", "inscripcion");

            entity.HasKey(e => e.CodigoArea)
                  .HasName("PK_Areax");

            entity.Property(e => e.CodigoArea)
                  .ValueGeneratedNever();

            entity.Property(e => e.DesArea)
                  .HasMaxLength(50)
                  .IsUnicode(false);
        });

        // ==========================================
        // Configuración: VessperienciasPersonal
        // ==========================================
        modelBuilder.Entity<VessperienciasPersonal>(entity =>
        {
            entity.ToTable("VessperienciasPersonal", "inscripcion");

            entity.HasKey(e => e.CodigoPersonal)
                  .HasName("PK_Personalx_1");

            entity.Property(e => e.CodigoPersonal)
                  .ValueGeneratedNever();

            entity.Property(e => e.ApeNom)
                  .HasMaxLength(250)
                  .IsUnicode(false);

            // Relación FK_VessperienciasPersonal_VessperienciasArea
            entity.HasOne(d => d.Area)
                  .WithMany(p => p.Personales)
                  .HasForeignKey(d => d.CodigoArea)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_VessperienciasPersonal_VessperienciasArea");
        });

        // ==========================================
        // Configuración: VessperienciasPonencia
        // ==========================================
        modelBuilder.Entity<VessperienciasPonencia>(entity =>
        {
            entity.ToTable("VessperienciasPonencia", "inscripcion");

            entity.HasKey(e => e.CodigoPonencia)
                  .HasName("PK_VessperienciasPonencia");

            entity.Property(e => e.CodigoPonencia)
                  .ValueGeneratedNever();

            entity.Property(e => e.Tema)
                  .HasColumnType("varchar(max)")
                  .IsUnicode(false);

            entity.Property(e => e.Vacantes);
        });

        // ==========================================
        // Configuración: Vessperiencias (Tabla intermedia)
        // ==========================================
        modelBuilder.Entity<Vessperiencias>(entity =>
        {
            entity.ToTable("Vessperiencias", "inscripcion");

            // Clave Primaria Compuesta
            entity.HasKey(e => new { e.CodigoPersonal, e.CodigoPonencia })
                  .HasName("PK_Vessperiencias");

            // Relación FK_Vessperiencias_VessperienciasPersonal
            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.Vessperiencias)
                  .HasForeignKey(d => d.CodigoPersonal)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Vessperiencias_VessperienciasPersonal");

            // Relación FK_Vessperiencias_VessperienciasPonencia
            entity.HasOne(d => d.Ponencia)
                  .WithMany(p => p.Vessperiencias)
                  .HasForeignKey(d => d.CodigoPonencia)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Vessperiencias_VessperienciasPonencia");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.ToTable("EventoPapaPersonal", "inscripcion");

            entity.HasKey(e => e.CodigoPersonal)
                  .HasName("PK_EventoPapaPersonal_1");

            entity.Property(e => e.CodigoPersonal)
                  .ValueGeneratedNever();

            entity.Property(e => e.NombreCompleto)
                  .HasColumnType("varchar(250)")
                  .UseCollation("Modern_Spanish_CI_AS");

            entity.Property(e => e.FechaNacimiento)
                  .HasColumnType("date");

            entity.Property(e => e.NumeroDocumento)
                  .HasColumnType("varchar(100)");

            entity.Property(e => e.Correo)
                  .HasColumnType("varchar(150)");
        });

        // 2. Configuración de EventoPapaHorario
        modelBuilder.Entity<EventoPapaHorario>(entity =>
        {
            entity.ToTable("EventoPapaHorario", "inscripcion");

            entity.HasKey(e => e.CodigoHorarioEventoPapa)
                  .HasName("PK_EventoPapaHorario");

            entity.Property(e => e.CodigoHorarioEventoPapa)
                  .ValueGeneratedNever();

            entity.Property(e => e.Descripcion)
                  .HasColumnType("varchar(max)")
                  .UseCollation("Modern_Spanish_CI_AS");

            entity.Property(e => e.Vacantes)
                  .HasColumnType("int");
        });

        // 3. Configuración de EventoPapa (Tabla con Clave Compuesta y Claves Foráneas)
        modelBuilder.Entity<EventoPapa>(entity =>
        {
            entity.ToTable("EventoPapa", "inscripcion");

            // Clave primaria compuesta
            entity.HasKey(e => new { e.CodigoPersonal, e.CodigoHorarioEventoPapa })
                  .HasName("PK_EventoPapa");

            entity.Property(e => e.FechaRegistro)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");

            // Relación 1:N entre Personal y EventoPapa (FK_EventoPapa_Personal)
            entity.HasOne(d => d.Personal)
                  .WithMany(p => p.EventosPapa)
                  .HasForeignKey(d => d.CodigoPersonal)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_EventoPapa_Personal");

            // Relación 1:N entre EventoPapaHorario y EventoPapa (FK_EventoPapa_EventoPapaHorario)
            entity.HasOne(d => d.EventoPapaHorario)
                  .WithMany(p => p.EventosPapa)
                  .HasForeignKey(d => d.CodigoHorarioEventoPapa)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_EventoPapa_EventoPapaHorario");
        });
    }
}
