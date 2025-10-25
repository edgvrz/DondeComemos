<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

>>>>>>> b808e6f (Avance Mauricio Benavente)
namespace DondeComemos.Models
{
    public class Restaurante
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public int Rating { get; set; } = 0;
    }
}
=======
        
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
        [Column(TypeName = "REAL")] 
        public double Rating { get; set; }
        
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
        public string? RangoPrecios { get; set; }
        
        [Display(Name = "Sitio Web")]
        public string? SitioWeb { get; set; }
        
        [Display(Name = "Facebook")]
        public string? Facebook { get; set; }
        
        [Display(Name = "Instagram")]
        public string? Instagram { get; set; }
        
        [Display(Name = "Twitter")]
        public string? Twitter { get; set; }
        
        [Display(Name = "Destacado")]
        public bool Destacado { get; set; } = false;
        
        // Navigation Properties
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
        
        [NotMapped]
        [Display(Name = "Subir Imagen")]
        public IFormFile? ImagenArchivo { get; set; }
        
        // Propiedades calculadas
        [NotMapped]
            public double RatingPromedio => Resenas.Any()
            ? (double)Resenas.Average(r => r.Calificacion)
            : Rating;        
        [NotMapped]
        public int TotalResenas => Resenas.Count;
    }
}
>>>>>>> b808e6f (Avance Mauricio Benavente)
