using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DondeComemos.Models
{
    public class Producto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; } = string.Empty;
        
        [Display(Name = "Descripción")]
        [StringLength(500)]
        public string? Descripcion { get; set; }
        
        [Required(ErrorMessage = "El precio es obligatorio")]
        [Display(Name = "Precio")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }
        
        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La categoría es obligatoria")]
        public string Categoria { get; set; } = "Plato Principal";
        
        [Display(Name = "Disponible")]
        public bool Disponible { get; set; } = true;
        
        [Display(Name = "Imagen")]
        public string? ImagenUrl { get; set; }
        
        // Foreign Key
        [Required]
        public int RestauranteId { get; set; }
        
        // Navigation Property
        public virtual Restaurante? Restaurante { get; set; }
        
        [NotMapped]
        [Display(Name = "Subir Imagen")]
        public IFormFile? ImagenArchivo { get; set; }
    }
}