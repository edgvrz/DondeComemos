using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DondeComemos.Models
{
    public class Restaurante
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del Restaurante")]
        public string Nombre { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;
        
        [Display(Name = "URL de Imagen")]
        public string ImagenUrl { get; set; } = string.Empty;
        
        [Required]
        [Range(0, 5, ErrorMessage = "El rating debe estar entre 0 y 5")]
        [Display(Name = "Calificación")]
        public int Rating { get; set; } = 0;
        
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }
        
        [Display(Name = "Horario")]
        public string? Horario { get; set; }
        
        [Display(Name = "Tipo de Cocina")]
        public string? TipoCocina { get; set; }
        
        [Display(Name = "Latitud")]
        public double? Latitud { get; set; }
        
        [Display(Name = "Longitud")]
        public double? Longitud { get; set; }
        
        [Display(Name = "Rango de Precios")]
        public string? RangoPrecios { get; set; } // "$", "$$", "$$$", "$$$$"
        
        // Navigation Properties
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
        
        [NotMapped]
        [Display(Name = "Subir Imagen")]
        public IFormFile? ImagenArchivo { get; set; }
    }
}