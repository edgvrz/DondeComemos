<<<<<<< HEAD
using DondeComemos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

=======
﻿using DondeComemos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

<<<<<<< HEAD
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
namespace DondeComemos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contacto> Contactos { get; set; } = null!;
        public DbSet<Restaurante> Restaurantes { get; set; } = null!;
<<<<<<< HEAD
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<Sugerencia> Sugerencias { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Resena> Resenas { get; set; } = null!;
        public DbSet<Notificacion> Notificaciones { get; set; } = null!;
        public DbSet<Favorito> Favoritos { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;
        public DbSet<ReservaProducto> ReservaProductos { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Configuración Restaurante-Producto
            builder.Entity<Producto>()
                .HasOne(p => p.Restaurante)
                .WithMany(r => r.Productos)
                .HasForeignKey(p => p.RestauranteId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configuración Restaurante-Reseña
            builder.Entity<Resena>()
                .HasOne(r => r.Restaurante)
                .WithMany(res => res.Resenas)
                .HasForeignKey(r => r.RestauranteId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configuración Favorito-Restaurante
            builder.Entity<Favorito>()
                .HasOne(f => f.Restaurante)
                .WithMany()
                .HasForeignKey(f => f.RestauranteId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Índice único para evitar favoritos duplicados
            builder.Entity<Favorito>()
                .HasIndex(f => new { f.UserId, f.RestauranteId })
                .IsUnique();
            
            // Configuración Reserva-Restaurante
            builder.Entity<Reserva>()
                .HasOne(r => r.Restaurante)
                .WithMany()
                .HasForeignKey(r => r.RestauranteId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configuración ReservaProducto
            builder.Entity<ReservaProducto>()
                .HasOne(rp => rp.Reserva)
                .WithMany(r => r.ProductosReservados)
                .HasForeignKey(rp => rp.ReservaId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<ReservaProducto>()
                .HasOne(rp => rp.Producto)
                .WithMany()
                .HasForeignKey(rp => rp.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            // Configuración de decimales para SQLite
            builder.Entity<Restaurante>()
                .Property(r => r.Rating)
                .HasColumnType("REAL");
            
            builder.Entity<Resena>()
                .Property(r => r.Calificacion)
                .HasConversion<double>();
            
            builder.Entity<Resena>()
                .Property(r => r.CalidadComida)
                .HasConversion<double?>();
            
            builder.Entity<Resena>()
                .Property(r => r.Servicio)
                .HasConversion<double?>();
            
            builder.Entity<Resena>()
                .Property(r => r.Ambiente)
                .HasConversion<double?>();
            
            builder.Entity<Resena>()
                .Property(r => r.RelacionPrecio)
                .HasConversion<double?>();
        }
    }
}
=======

        // 🆕 Nueva tabla para los favoritos (watchlist)
        public DbSet<Watchlist> Watchlists { get; set; }

    }
}
=======
namespace DondeComemos.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contacto> Contactos { get; set; } = null!;
    public DbSet<Restaurante> Restaurantes { get; set; } = null!;
    public DbSet<UserProfile> UserProfiles { get; set; } = null!;
    public DbSet<Sugerencia> Sugerencias { get; set; } = null!;
    public DbSet<Producto> Productos { get; set; } = null!;
    public DbSet<Resena> Resenas { get; set; } = null!;
    public DbSet<Notificacion> Notificaciones { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configuración de la relación Restaurante-Producto
        builder.Entity<Producto>()
            .HasOne(p => p.Restaurante)
            .WithMany(r => r.Productos)
            .HasForeignKey(p => p.RestauranteId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Configuración de la relación Restaurante-Reseña
        builder.Entity<Resena>()
            .HasOne(r => r.Restaurante)
            .WithMany(res => res.Resenas)
            .HasForeignKey(r => r.RestauranteId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Configuración de decimales para Rating
        builder.Entity<Restaurante>()
            .Property(r => r.Rating)
            .HasColumnType("decimal(3,2)");
        
        builder.Entity<Resena>()
            .Property(r => r.Calificacion)
            .HasColumnType("decimal(3,2)");
        
        builder.Entity<Resena>()
            .Property(r => r.CalidadComida)
            .HasColumnType("decimal(3,2)");
        
        builder.Entity<Resena>()
            .Property(r => r.Servicio)
            .HasColumnType("decimal(3,2)");
        
        builder.Entity<Resena>()
            .Property(r => r.Ambiente)
            .HasColumnType("decimal(3,2)");
        
        builder.Entity<Resena>()
            .Property(r => r.RelacionPrecio)
            .HasColumnType("decimal(3,2)");
    }
}
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
