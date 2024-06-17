using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace CrudICBF.Models;

public partial class IcbfContext : DbContext
{
    public IcbfContext()
    {
    }

    public IcbfContext(DbContextOptions<IcbfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acudiente> Acudientes { get; set; }

    public virtual DbSet<Jardin> Jardins { get; set; }

    public virtual DbSet<Madrecom> Madrecoms { get; set; }

    public virtual DbSet<Ninio> Ninios { get; set; }

    public virtual DbSet<Registroasistencium> Registroasistencia { get; set; }

    public virtual DbSet<Registroavanceacademico> Registroavanceacademicos { get; set; }

//  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=localhost;port=3306;database=ICBF;uid=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.32-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Acudiente>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("PRIMARY");

            entity.ToTable("acudiente");

            entity.Property(e => e.Cedula)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Celular).HasMaxLength(20);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Jardin>(entity =>
        {
            entity.HasKey(e => e.IdentificadorJardin).HasName("PRIMARY");

            entity.ToTable("jardin");

            entity.Property(e => e.IdentificadorJardin).HasColumnType("int(11)");
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Estado).HasColumnType("enum('Aprobado','En trámite','Negado')");
            entity.Property(e => e.NombreJardin).HasMaxLength(100);
        });

        modelBuilder.Entity<Madrecom>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("PRIMARY");

            entity.ToTable("madrecom");

            entity.Property(e => e.Cedula)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Ninio>(entity =>
        {
            entity.HasKey(e => e.RegistroNiup).HasName("PRIMARY");

            entity.ToTable("ninio");

            entity.HasIndex(e => e.IdentificacionAcudiente, "IdentificacionAcudiente");

            entity.HasIndex(e => e.IdentificacionMadreCom, "IdentificacionMadreCom");

            entity.HasIndex(e => e.IdentificadorJardin, "IdentificadorJardin");

            entity.Property(e => e.RegistroNiup)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("RegistroNIUP");
            entity.Property(e => e.CiudadNacimiento).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Eps)
                .HasMaxLength(50)
                .HasColumnName("EPS");
            entity.Property(e => e.IdentificacionAcudiente).HasColumnType("int(11)");
            entity.Property(e => e.IdentificacionMadreCom).HasColumnType("int(11)");
            entity.Property(e => e.IdentificadorJardin).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.TipoSangre).HasMaxLength(5);

            entity.HasOne(d => d.IdentificacionAcudienteNavigation).WithMany(p => p.Ninios)
                .HasForeignKey(d => d.IdentificacionAcudiente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ninio_ibfk_1");

            entity.HasOne(d => d.IdentificacionMadreComNavigation).WithMany(p => p.Ninios)
                .HasForeignKey(d => d.IdentificacionMadreCom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ninio_ibfk_2");

            entity.HasOne(d => d.IdentificadorJardinNavigation).WithMany(p => p.Ninios)
                .HasForeignKey(d => d.IdentificadorJardin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ninio_ibfk_3");
        });

        modelBuilder.Entity<Registroasistencium>(entity =>
        {
            entity.HasKey(e => e.RegistroAsistencia).HasName("PRIMARY");

            entity.ToTable("registroasistencia");

            entity.HasIndex(e => e.IdentificacionNino, "IdentificacionNino");

            entity.Property(e => e.RegistroAsistencia).HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasMaxLength(20);
            entity.Property(e => e.IdentificacionNino).HasColumnType("int(11)");

            entity.HasOne(d => d.IdentificacionNinoNavigation).WithMany(p => p.Registroasistencia)
                .HasForeignKey(d => d.IdentificacionNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registroasistencia_ibfk_1");
        });

        modelBuilder.Entity<Registroavanceacademico>(entity =>
        {
            entity.HasKey(e => e.RegistroAvance).HasName("PRIMARY");

            entity.ToTable("registroavanceacademico");

            entity.HasIndex(e => e.IdentificacionNino, "IdentificacionNino");

            entity.Property(e => e.RegistroAvance).HasColumnType("int(11)");
            entity.Property(e => e.AnioEscolar).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.IdentificacionNino).HasColumnType("int(11)");
            entity.Property(e => e.Nivel).HasMaxLength(20);
            entity.Property(e => e.Notas).HasMaxLength(5);

            entity.HasOne(d => d.IdentificacionNinoNavigation).WithMany(p => p.Registroavanceacademicos)
                .HasForeignKey(d => d.IdentificacionNino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registroavanceacademico_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
