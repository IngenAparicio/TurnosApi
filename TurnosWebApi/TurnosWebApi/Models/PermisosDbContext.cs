using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TurnosWebApi.Models
{
    public partial class PermisosDbContext : DbContext
    {
        public PermisosDbContext()
        {
        }

        public PermisosDbContext(DbContextOptions<PermisosDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<ConsultaServicios> ConsultaServicios { get; set; } = null!;
        public virtual DbSet<Comercios> Comercios { get; set; } = null!;
        public virtual DbSet<Servicios> Servicios { get; set; } = null!;
        public virtual DbSet<Turnos> Turnos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comercios>(entity =>
            {
                entity.HasKey(e => e.IdComercio);

                entity.ToTable("comercios");

                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");

                entity.Property(e => e.AforoMaximo)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("aforo_maximo");

                entity.Property(e => e.NomComercio)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("nom_comercio");
            });

            modelBuilder.Entity<Servicios>(entity =>
            {
                entity.HasKey(e => e.IdServicio);

                entity.ToTable("servicios");

                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

                entity.Property(e => e.Duracion)
                    .HasColumnType("time(0)")
                    .HasColumnName("duracion");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.HoraApertura)
                    .HasColumnType("time(0)")
                    .HasColumnName("hora_apertura");

                entity.Property(e => e.HoraCierre)
                    .HasColumnType("time(0)")
                    .HasColumnName("hora_cierre");

                entity.Property(e => e.IdComercio).HasColumnName("id_comercio");

                entity.Property(e => e.NomServicio)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("nom_servicio");

                entity.HasOne(d => d.IdComercioNavigation)
                    .WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdComercio)
                    .HasConstraintName("FK_servicios_comercios");
            });

            modelBuilder.Entity<Turnos>(entity =>
            {
                entity.HasKey(e => e.IdTurno);

                entity.ToTable("turnos");

                entity.Property(e => e.IdTurno).HasColumnName("id_turno");

                entity.Property(e => e.Estado)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaTurno)
                    .HasColumnType("date")
                    .HasColumnName("fecha_turno");

                entity.Property(e => e.HoraFin)
                    .HasColumnType("time(0)")
                    .HasColumnName("hora_fin");

                entity.Property(e => e.HoraInicio)
                    .HasColumnType("time(0)")
                    .HasColumnName("hora_inicio");

                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

                entity.HasOne(d => d.IdServicioNavigation)
                    .WithMany(p => p.Turnos)
                    .HasForeignKey(d => d.IdServicio)
                    .HasConstraintName("FK_turnos_servicios");
            });

            modelBuilder.Entity<ConsultaServicios>(entity =>
            {
                entity.HasNoKey();                

                entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

                entity.Property(e => e.Duracion)
                    .HasColumnType("time(0)")
                    .HasColumnName("duracion");

                entity.Property(e => e.FechaFin).HasColumnType("date");

                entity.Property(e => e.FechaInicio).HasColumnType("date");

                entity.Property(e => e.HoraApertura)
                    .HasColumnType("time(0)")
                    .HasColumnName("hora_apertura");

                entity.Property(e => e.HoraCierre)
                    .HasColumnType("time(0)")
                    .HasColumnName("hora_cierre");                

                entity.Property(e => e.NomServicio)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("nom_servicio");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
