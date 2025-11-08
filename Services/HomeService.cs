<<<<<<< HEAD
using DondeComemos.Models;
using DondeComemos.Data;
using Microsoft.EntityFrameworkCore;
=======
using System.Collections.Generic;
<<<<<<< HEAD
using DondeComemos.Models;
=======
using System.Linq;
using DondeComemos.Models;
using DondeComemos.Data;
using Microsoft.EntityFrameworkCore;
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7

namespace DondeComemos.Services
{
    public interface IHomeService
    {
        HomeViewModel GetHomeData();
    }

    public class HomeService : IHomeService
    {
<<<<<<< HEAD
=======
<<<<<<< HEAD
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
=======
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public HomeViewModel GetHomeData()
        {
<<<<<<< HEAD
            // Obtener 3 restaurantes aleatorios
            var todosRestaurantes = _context.Restaurantes.ToList();
            
            List<RestaurantCard> restaurantesAleatorios;
            
            if (todosRestaurantes.Count > 0)
            {
                var random = new Random();
                restaurantesAleatorios = todosRestaurantes
                    .OrderBy(x => random.Next())
                    .Take(3)
                    .Select(r => new RestaurantCard
                    {
                        Nombre = r.Nombre,
                        Descripcion = r.Descripcion,
                        ImagenUrl = r.ImagenUrl,
                        Rating = (int)r.Rating
                    })
                    .ToList();
            }
            else
            {
                // Restaurantes de ejemplo si no hay datos
=======
            // Obtener 3 restaurantes aleatorios de la base de datos
            var restaurantesAleatorios = _context.Restaurantes
    .ToList() // Trae todos los restaurantes a memoria
    .OrderBy(r => Guid.NewGuid()) // Ordena aleatoriamente en memoria
    .Take(3)
    .Select(r => new RestaurantCard
    {
        Nombre = r.Nombre,
        Descripcion = r.Descripcion,
        ImagenUrl = r.ImagenUrl
    })
    .ToList();

            // Si no hay suficientes restaurantes, usar datos de ejemplo
            if (restaurantesAleatorios.Count == 0)
            {
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                restaurantesAleatorios = new List<RestaurantCard>
                {
                    new RestaurantCard 
                    { 
<<<<<<< HEAD
                        Nombre = "Restaurante Ejemplo 1", 
                        Descripcion = "Excelente comida peruana con sabor tradicional", 
                        ImagenUrl = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=600", 
=======
                        Nombre = "Restaurante 1", 
                        Descripcion = "Breve descripción", 
                        ImagenUrl = "/img/rest1.jpg", 
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                        Rating = 5 
                    },
                    new RestaurantCard 
                    { 
<<<<<<< HEAD
                        Nombre = "Restaurante Ejemplo 2", 
                        Descripcion = "Fusión de sabores internacionales", 
                        ImagenUrl = "https://images.unsplash.com/photo-1552566626-52f8b828add9?w=600", 
=======
                        Nombre = "Restaurante 2", 
                        Descripcion = "Información", 
                        ImagenUrl = "/img/rest2.jpg", 
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                        Rating = 5 
                    },
                    new RestaurantCard 
                    { 
<<<<<<< HEAD
                        Nombre = "Restaurante Ejemplo 3", 
                        Descripcion = "Ambiente acogedor y platos deliciosos", 
                        ImagenUrl = "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=600", 
=======
                        Nombre = "Restaurante 3", 
                        Descripcion = "Breve descripción", 
                        ImagenUrl = "/img/rest3.jpg", 
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                        Rating = 5 
                    },
                };
            }

            return new HomeViewModel
            {
                Favoritos = restaurantesAleatorios,
                Reseñas = new List<Testimonial>
                {
                    new Testimonial 
                    { 
<<<<<<< HEAD
                        Texto = "Excelente servicio y comida deliciosa. ¡Volveré sin duda!", 
                        Usuario = "María García", 
                        Rol = "Cliente Frecuente", 
                        ImagenUrl = "https://ui-avatars.com/api/?name=Maria+Garcia&background=667eea&color=fff&size=100" 
                    },
                    new Testimonial 
                    { 
                        Texto = "Ambiente acogedor y precios justos. Recomendado 100%", 
                        Usuario = "Carlos Rodríguez", 
                        Rol = "Food Blogger", 
                        ImagenUrl = "https://ui-avatars.com/api/?name=Carlos+Rodriguez&background=764ba2&color=fff&size=100" 
                    },
                    new Testimonial 
                    { 
                        Texto = "Gran variedad de opciones gastronómicas y excelente atención", 
                        Usuario = "Ana Martínez", 
                        Rol = "Crítica Gastronómica", 
                        ImagenUrl = "https://ui-avatars.com/api/?name=Ana+Martinez&background=f093fb&color=fff&size=100" 
                    },
=======
                        Texto = "Excelente servicio y comida deliciosa", 
                        Usuario = "María García", 
                        Rol = "Cliente Frecuente", 
                        ImagenUrl = "/img/user1.jpg" 
                    },
                    new Testimonial 
                    { 
                        Texto = "Ambiente acogedor y precios justos", 
                        Usuario = "Carlos Rodríguez", 
                        Rol = "Food Blogger", 
                        ImagenUrl = "/img/user2.jpg" 
                    },
                    new Testimonial 
                    { 
                        Texto = "Gran variedad de opciones gastronómicas", 
                        Usuario = "Ana Martínez", 
                        Rol = "Crítica Gastronómica", 
                        ImagenUrl = "/img/user3.jpg" 
                    },
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
                }
            };
        }
    }
<<<<<<< HEAD
}
=======
<<<<<<< HEAD
}
=======
}
>>>>>>> b808e6f (Avance Mauricio Benavente)
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
