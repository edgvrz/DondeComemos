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

    // Si ya tienes Contacto en Models, mantenla; si no, quítala.
    public DbSet<Contacto> Contactos { get; set; } = null!;

    public DbSet<Restaurante> Restaurantes { get; set; } = null!;

}
