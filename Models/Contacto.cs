using System.ComponentModel.DataAnnotations;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations.Schema;

namespace DondeComemos.Models
{
    [Table("Contactos")]
=======
using System.ComponentModel.DataAnnotations.Schema;     

namespace DondeComemos.Models
{
    [Table("Contactos")] // Nombre de la tabla en la base de datos
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
<<<<<<< HEAD
        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El email es obligatorio")]
        public string? Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string? Mensaje { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La puntuación de satisfacción es obligatoria")]
        public string? Satisfaccion { get; set; } = string.Empty;
        
        public bool InPositive { get; set; } = true;
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
=======
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El email es obligatorio")]
        public string? Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "El mensaje es obligatorio")]
        public string? Mensaje { get; set; } = string.Empty;
        [Required(ErrorMessage = "La satisfacción del 1 -10 es obligatoria")]
        public string? Satisfaccion { get; set; } = string.Empty;
        public bool InPositive { get; set; } = true;    
        public DateTime Fecha { get; set; } = DateTime.Now;

    }
}
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
