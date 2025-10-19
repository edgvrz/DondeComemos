using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DondeComemos.Models
{
    public class Watchlist
    {
        [Key]
        public int Id { get; set; }

        // Relación con el usuario (cliente)
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        // Relación con el restaurante
        [Required]
        public int RestauranteId { get; set; }

        [ForeignKey("RestauranteId")]
        public Restaurante? Restaurante { get; set; }

        public DateTime FechaAgregado { get; set; } = DateTime.Now;
    }
}
