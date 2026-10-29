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

    public DbSet<SharingDayHorario> SharingDayHorarios { get; set; } = null!;
    public DbSet<SharingDayTurno> SharingDayTurnos { get; set; } = null!;
    public DbSet<SharingDay> SharingDays { get; set; } = null!;
    public DbSet<Curso> Cursos { get; set; } = null!;
    public DbSet<Grado> Grados { get; set; } = null!;
    public DbSet<Seccion> Secciones { get; set; } = null!;
    public DbSet<Alumno> Alumnos { get; set; } = null!;
    public DbSet<Pariente> Parientes { get; set; } = null!;
    public DbSet<CuadroFamiliar> CuadroFamiliares { get; set; } = null!;

    public DbSet<Salon> Salones { get; set; } = null!;
    public DbSet<Matricula> Matriculas { get; set; } = null!;

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

        modelBuilder.Entity<SharingDayHorario>(entity =>
        {
            entity.ToTable("SharingDayHorario", "inscripcion");
            entity.HasKey(e => e.IdharingDayHorario);

            entity.Property(e => e.HoraInicio).HasMaxLength(100);
            entity.Property(e => e.HoraFin).HasMaxLength(100);
            entity.Property(e => e.SwActivo).HasDefaultValue(true);

            entity.HasIndex(e => new { e.HoraInicio, e.HoraFin })
                  .IsUnique()
                  .HasDatabaseName("UC_SharingDayHorario");
        });

        // 2. Configuración de SharingDayTurno
        modelBuilder.Entity<SharingDayTurno>(entity =>
        {
            entity.ToTable("SharingDayTurno", "inscripcion");
            entity.HasKey(e => e.IdSharingDayTurno);

            entity.Property(e => e.Vacantes).HasDefaultValue(0);
            entity.Property(e => e.SwActivo).HasDefaultValue(true);

            // Índices
            entity.HasIndex(e => new { e.IdharingDayHorario, e.CodigoGrado, e.CodigoSeccion })
                  .IsUnique()
                  .HasFilter("[SwActivo] = 1")
                  .HasDatabaseName("UX_SharingDayTurno_Activo");

            // Relaciones FK
            entity.HasOne(d => d.SharingDayHorario)
                  .WithMany(p => p.SharingDayTurnos)
                  .HasForeignKey(d => d.IdharingDayHorario)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharingDayTurno_SharingDayHorario");

            entity.HasOne(d => d.Curso)
                  .WithMany(p => p.SharingDayTurnos)
                  .HasForeignKey(d => d.CodigoCurso)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharingDayTurno_Curso");

            entity.HasOne(d => d.Grado)
                  .WithMany(p => p.SharingDayTurnos)
                  .HasForeignKey(d => d.CodigoGrado)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharingDayTurno_Grado");

            entity.HasOne(d => d.Seccion)
                  .WithMany(p => p.SharingDayTurnos)
                  .HasForeignKey(d => d.CodigoSeccion)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharingDayTurno_Seccion");
        });

        // 3. Configuración de SharingDay y sus relaciones explícitas
        modelBuilder.Entity<SharingDay>(entity =>
        {
            entity.ToTable("SharingDay", "inscripcion");
            entity.HasKey(e => e.IdSharingDay);

            entity.HasIndex(e => e.CodigoAlumno).IsUnique();

            entity.Property(e => e.FechaRegistro)
                  .HasColumnType("datetime")
                  .HasDefaultValueSql("GETDATE()");

            // FK_SharingDay_SharingDayTurno
            entity.HasOne(d => d.SharingDayTurno)
                  .WithMany(p => p.SharingDays)
                  .HasForeignKey(d => d.IdSharingDayTurno)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharingDay_SharingDayTurno");

            // FK_SharingDay_Pariente -> dbo.Pariente
            entity.HasOne(d => d.Pariente)
                  .WithMany(p => p.SharingDays)
                  .HasForeignKey(d => d.CodigoPariente)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharingDay_Pariente");

            // FK_SharingDay_Alumno -> dbo.Alumno
            entity.HasOne(d => d.Alumno)
                  .WithMany(p => p.SharingDays)
                  .HasForeignKey(d => d.CodigoAlumno)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_SharingDay_Alumno");
        });

        // 4. Curso
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.ToTable("Curso", "notas");
            entity.HasKey(e => e.CodigoCurso).HasName("PK_Curso");
            entity.Property(e => e.CodCursoAnt).HasColumnType("char(3)");
        });

        // 5. Grado
        modelBuilder.Entity<Grado>(entity =>
        {
            entity.ToTable("Grado", "dbo");
            entity.HasKey(e => e.CodigoGrado).HasName("PK_Grado");
        });

        // 6. Seccion
        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.ToTable("Seccion", "dbo");
            entity.HasKey(e => e.CodigoSeccion).HasName("PK_Seccion");
            entity.Property(e => e.Descripcion).HasColumnType("char(1)");
            entity.Property(e => e.Codigo_Ant).HasColumnType("char(3)");
        });

        // 10. Salon
        modelBuilder.Entity<Salon>(entity =>
        {
            entity.ToTable("Salon", "dbo");
            entity.HasKey(e => e.CodigoSalon).HasName("PK_Salon_1");

            entity.Property(e => e.NombreSalon).HasColumnType("char(20)");
            entity.Property(e => e.CodigoTurno).HasColumnType("char(1)");
            entity.Property(e => e.CodigoCentroCosto).HasColumnType("char(2)");
            entity.Property(e => e.CodigoNivel).HasColumnType("char(2)");

            entity.HasOne(d => d.Grado)
                  .WithMany(p => p.Salones)
                  .HasForeignKey(d => d.CodigoGrado)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Seccion)
                  .WithMany(p => p.Salones)
                  .HasForeignKey(d => d.CodigoSeccion)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });

        // 11. Matricula
        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.ToTable("Matricula", "dbo");
            entity.HasKey(e => e.CodigoMatricula).HasName("PK_matricula");

            entity.Property(e => e.CodigoCentroCosto).HasColumnType("char(2)");
            entity.Property(e => e.CodigoTurno).HasColumnType("char(1)");
            entity.Property(e => e.CodigoNivel).HasColumnType("char(2)");
            entity.Property(e => e.CodigoAlumno).HasColumnType("char(8)");
            entity.Property(e => e.CodigoSituacionEvaluacion).HasColumnType("char(2)");

            // Relaciones FK
            entity.HasOne(d => d.Salon)
                  .WithMany(p => p.Matriculas)
                  .HasForeignKey(d => d.CodigoSalon)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Alumno)
                  .WithMany(p => p.Matriculas)
                  .HasForeignKey(d => d.CodigoAlumno)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Grado)
                  .WithMany(p => p.Matriculas)
                  .HasForeignKey(d => d.CodigoGrado)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            // Índices no agrupados reflejando los scripts DDL
            entity.HasIndex(e => new { e.CodigoSalon, e.CodigoAlumno, e.SwActivo })
                  .HasDatabaseName("index_Matricula2");

            entity.HasIndex(e => e.SwActivo)
                  .IncludeProperties(e => new { e.CodigoAlumno, e.CodigoSalon })
                  .HasDatabaseName("index_matricula");
        });

        // 7. Alumno
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.ToTable("Alumno", "dbo");
            entity.HasKey(e => e.CodigoAlumno).HasName("PK_Alumno");

            entity.Property(e => e.SwExoneradoDocumentos).HasDefaultValue(false);
            entity.Property(e => e.SwExoneradoCuotaIngreso).HasDefaultValue(false);
        });

        // 8. Pariente
        modelBuilder.Entity<Pariente>(entity =>
        {
            entity.ToTable("Pariente", "admision");
            entity.HasKey(e => e.CodigoPariente).HasName("PK_Pariente_1");
        });

        // 9. CuadroFamiliar (Clave Compuesta + FKs a Pariente y Alumno)
        modelBuilder.Entity<CuadroFamiliar>(entity =>
        {
            entity.ToTable("CuadroFamiliar", "admision");

            // Clave primaria compuesta de 3 campos
            entity.HasKey(e => new { e.CodigoFamilia, e.CodigoPariente, e.CodigoPostulante })
                  .HasName("PK_CuadroFamiliar_1");

            entity.Property(e => e.CodigoFamilia).HasColumnType("char(8)");
            entity.Property(e => e.CodigoPariente).HasColumnType("char(8)");
            entity.Property(e => e.CodigoPostulante).HasColumnType("char(8)");

            // Relación N:1 con Pariente
            entity.HasOne(d => d.Pariente)
                  .WithMany(p => p.CuadroFamiliares)
                  .HasForeignKey(d => d.CodigoPariente)
                  .OnDelete(DeleteBehavior.ClientSetNull);

            // Relación N:1 con Alumno (CodigoPostulante -> CodigoAlumno)
            entity.HasOne(d => d.Postulante)
                  .WithMany(p => p.CuadroFamiliares)
                  .HasForeignKey(d => d.CodigoPostulante)
                  .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
}
