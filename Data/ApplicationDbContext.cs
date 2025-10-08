using DondeComemos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Configuración de la relación Restaurante-Producto
        builder.Entity<Producto>()
            .HasOne(p => p.Restaurante)
            .WithMany(r => r.Productos)
            .HasForeignKey(p => p.RestauranteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}