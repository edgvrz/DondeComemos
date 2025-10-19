using DondeComemos.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        // 🆕 Nueva tabla para los favoritos (watchlist)
        public DbSet<Watchlist> Watchlists { get; set; }

    }
}
