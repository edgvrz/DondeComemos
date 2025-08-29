using System.Collections.Generic;
using DondeComemos.Models;

namespace DondeComemos.Services
{
    public interface IHomeService
    {
        HomeViewModel GetHomeData();
    }

    public class HomeService : IHomeService
    {
        public HomeViewModel GetHomeData()
        {
            return new HomeViewModel
            {
                Favoritos = new List<RestaurantCard>
                {
                    new RestaurantCard { Nombre = "Restaurante 1", Descripcion = "Breve descripción", ImagenUrl="/img/rest1.jpg", Rating=5 },
                    new RestaurantCard { Nombre = "Restaurante 2", Descripcion = "Información", ImagenUrl="/img/rest2.jpg", Rating=5 },
                    new RestaurantCard { Nombre = "Restaurante 3", Descripcion = "Breve descripción", ImagenUrl="/img/rest3.jpg", Rating=5 },
                },
                Reseñas = new List<Testimonial>
                {
                    new Testimonial { Texto = "Buena sazón", Usuario = "Hombre", Rol="Developer", ImagenUrl="/img/user1.jpg" },
                    new Testimonial { Texto = "Buen atendido", Usuario = "Mujer", Rol="Designer", ImagenUrl="/img/user2.jpg" },
                    new Testimonial { Texto = "Mucha variedad", Usuario = "Chef", Rol="Chef", ImagenUrl="/img/user3.jpg" },
                }
            };
        }
    }
}
