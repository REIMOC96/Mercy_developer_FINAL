using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MercDevs_ej2.Models;

public partial class MercyDeveloperContext : DbContext
{
    public MercyDeveloperContext()
    {
    }

    public MercyDeveloperContext(DbContextOptions<MercyDeveloperContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Datosfichatecnica> Datosfichatecnicas { get; set; }

    public virtual DbSet<Descripcionservicio> Descripcionservicios { get; set; }

    public virtual DbSet<Diagnosticosolucion> Diagnosticosolucions { get; set; }

    public virtual DbSet<Recepcionequipo> Recepcionequipos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) { 
        }
    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity
                .ToTable("cliente")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("idCliente");
            entity.Property(e => e.Apellido).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.Direccion).HasMaxLength(45);
            entity.Property(e => e.Estado).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Telefono).HasMaxLength(45);
        });

        modelBuilder.Entity<Datosfichatecnica>(entity =>
        {
            entity.HasKey(e => e.IdDatosFichaTecnica).HasName("PRIMARY");

            entity
                .ToTable("datosfichatecnica")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.RecepcionEquipoId, "fk_DatosFichaTecnica_RecepcionEquipo1_idx");

            entity.Property(e => e.IdDatosFichaTecnica)
                .HasColumnType("int(11)")
                .HasColumnName("idDatosFichaTecnica");
            entity.Property(e => e.AntivirusInstalado)
                .HasMaxLength(100)
                .HasColumnName("Antivirus Instalado");
            entity.Property(e => e.Estado).HasColumnType("int(11)");
            entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.LectorPdfinstalado)
                .HasComment("0:No Instalado ; 1: Instalado 2: No aplica")
                .HasColumnType("int(11)")
                .HasColumnName("LectorPDFInstalado");
            entity.Property(e => e.NavegadorWebInstalado)
                .HasComment("0:No instalado ; 1: Chrome ; 2: Firefox; 3: Chrome y Firefox")
                .HasColumnType("int(11)");
            entity.Property(e => e.PobservacionesRecomendaciones)
                .HasMaxLength(2000)
                .HasComment("Por el Tecnico")
                .HasColumnName("PObservacionesRecomendaciones");
            entity.Property(e => e.RecepcionEquipoId).HasColumnType("int(11)");
            entity.Property(e => e.Soinstalado)
                .HasComment("0:Windows 10 ; 1: Windows 11; 2: Linux")
                .HasColumnType("int(11)")
                .HasColumnName("SOInstalado");
            entity.Property(e => e.SuiteOfficeInstalada)
                .HasComment("0: Microsoft Office 2013 ; 1: Microsoft Office 2019 ; 2: Microsoft Office 365 ; 3: OpenOffice")
                .HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasColumnType("int(11)")
            .HasComment("0 : Inactivo ; 1: Activo ; 2: En proceso");

            entity.HasOne(d => d.RecepcionEquipo).WithMany(p => p.Datosfichatecnicas)
                .HasForeignKey(d => d.RecepcionEquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DatosFichaTecnica_RecepcionEquipo1");
        });

        modelBuilder.Entity<Descripcionservicio>(entity =>
        {
            entity.HasKey(e => e.IdDescServ).HasName("PRIMARY");

            entity
                .ToTable("descripcionservicio")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.ServicioIdServicio, "fk_DescripcionServicio_Servicio1_idx");

            entity.Property(e => e.IdDescServ)
                .HasColumnType("int(11)")
                .HasColumnName("idDescServ");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.ServicioIdServicio)
                .HasColumnType("int(11)")
                .HasColumnName("Servicio_idServicio");

            entity.HasOne(d => d.ServicioIdServicioNavigation).WithMany(p => p.Descripcionservicios)
                .HasForeignKey(d => d.ServicioIdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DescripcionServicio_Servicio1");
        });

        modelBuilder.Entity<Diagnosticosolucion>(entity =>
        {
            entity.HasKey(e => e.IdDiagnosticoSolucion).HasName("PRIMARY");

            entity
                .ToTable("diagnosticosolucion")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.DatosFichaTecnicaId, "fk_DiagnosticoSolucion_DatosFichaTecnica1_idx");

            entity.Property(e => e.IdDiagnosticoSolucion)
                .HasColumnType("int(11)")
                .HasColumnName("idDiagnosticoSolucion");
            entity.Property(e => e.DatosFichaTecnicaId).HasColumnType("int(11)");
            entity.Property(e => e.DescripcionDiagnostico).HasMaxLength(1000);
            entity.Property(e => e.DescripcionSolucion).HasMaxLength(1000);

            entity.HasOne(d => d.DatosFichaTecnica).WithMany(p => p.Diagnosticosolucions)
                .HasForeignKey(d => d.DatosFichaTecnicaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DiagnosticoSolucion_DatosFichaTecnica1");
        });

        modelBuilder.Entity<Recepcionequipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("recepcionequipo")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdCliente, "fk_RecepcionEquipo_Cliente1_idx");

            entity.HasIndex(e => e.IdServicio, "fk_RecepcionEquipo_Servicio1_idx");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Accesorio).HasMaxLength(45);
            entity.Property(e => e.CapacidadAlmacenamiento).HasMaxLength(45);
            entity.Property(e => e.CapacidadRam).HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasColumnType("int(11)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Grafico).HasMaxLength(45);
            entity.Property(e => e.IdCliente).HasColumnType("int(11)");
            entity.Property(e => e.IdServicio).HasColumnType("int(11)");
            entity.Property(e => e.MarcaPc).HasMaxLength(45);
            entity.Property(e => e.ModeloPc).HasMaxLength(45);
            entity.Property(e => e.Nserie)
                .HasMaxLength(45)
                .HasColumnName("NSerie");
            entity.Property(e => e.CPU).HasMaxLength(50).HasComment("String modelo CPU");
            entity.Property(e => e.TipoAlmacenamiento).HasColumnType("int(11)");
            entity.Property(e => e.TipoGpu).HasColumnType("int(11)");
            entity.Property(e => e.TipoPc).HasColumnType("int(11)").HasComment("0: Intel ; 1: Amd; 2: Otros;");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Recepcionequipos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RecepcionEquipo_Cliente1");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Recepcionequipos)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RecepcionEquipo_Servicio1");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity
                .ToTable("servicio")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.UsuarioIdUsuario, "fk_Servicio_Usuario_idx");

            entity.Property(e => e.IdServicio)
                .HasColumnType("int(11)")
                .HasColumnName("idServicio");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Precio).HasColumnType("int(11)");
            entity.Property(e => e.Sku).HasMaxLength(45);
            entity.Property(e => e.UsuarioIdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("Usuario_idUsuario");

            entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.UsuarioIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Servicio_Usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity
                .ToTable("usuario")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.Property(e => e.IdUsuario)
                .HasColumnType("int(11)")
                .HasColumnName("idUsuario");
            entity.Property(e => e.Apellido).HasMaxLength(45);
            entity.Property(e => e.Correo).HasMaxLength(45);
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
