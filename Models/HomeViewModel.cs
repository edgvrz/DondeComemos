<<<<<<< HEAD
=======
using System.Collections.Generic;

>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
namespace DondeComemos.Models
{
    public class HomeViewModel
    {
        public IEnumerable<RestaurantCard> Favoritos { get; set; } = new List<RestaurantCard>();
        public IEnumerable<Testimonial> Reseñas { get; set; } = new List<Testimonial>();
    }

    public class RestaurantCard
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public int Rating { get; set; }
    }

    public class Testimonial
    {
        public string Texto { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
