using DondeComemos.Models;
using DondeComemos.Data;
using Microsoft.EntityFrameworkCore;

namespace DondeComemos.Services
{
    public interface IHomeService
    {
        HomeViewModel GetHomeData();
    }

    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _context;

        public HomeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public HomeViewModel GetHomeData()
        {
            // Primero intentar obtener restaurantes destacados
            var restaurantesDestacados = _context.Restaurantes
                .Where(r => r.Destacado)
                .OrderByDescending(r => r.Rating)
                .Take(3)
                .Select(r => new RestaurantCard
                {
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    ImagenUrl = r.ImagenUrl,
                    Rating = (int)r.Rating
                })
                .ToList();

            // Si no hay suficientes destacados, complementar con los mejor calificados
            if (restaurantesDestacados.Count < 3)
            {
                var faltantes = 3 - restaurantesDestacados.Count;
                var mejorCalificados = _context.Restaurantes
                    .Where(r => !r.Destacado)
                    .OrderByDescending(r => r.Rating)
                    .Take(faltantes)
                    .Select(r => new RestaurantCard
                    {
                        Nombre = r.Nombre,
                        Descripcion = r.Descripcion,
                        ImagenUrl = r.ImagenUrl,
                        Rating = (int)r.Rating
                    })
                    .ToList();

                restaurantesDestacados.AddRange(mejorCalificados);
            }

            // Si aún no hay restaurantes, mostrar datos de ejemplo
            if (restaurantesDestacados.Count == 0)
            {
                restaurantesDestacados = new List<RestaurantCard>
                {
                    new RestaurantCard 
                    { 
                        Nombre = "Restaurante Ejemplo 1", 
                        Descripcion = "Excelente comida peruana con sabor tradicional", 
                        ImagenUrl = "https://images.unsplash.com/photo-1517248135467-4c7edcad34c4?w=600", 
                        Rating = 5 
                    },
                    new RestaurantCard 
                    { 
                        Nombre = "Restaurante Ejemplo 2", 
                        Descripcion = "Fusión de sabores internacionales", 
                        ImagenUrl = "https://images.unsplash.com/photo-1552566626-52f8b828add9?w=600", 
                        Rating = 5 
                    },
                    new RestaurantCard 
                    { 
                        Nombre = "Restaurante Ejemplo 3", 
                        Descripcion = "Ambiente acogedor y platos deliciosos", 
                        ImagenUrl = "https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=600", 
                        Rating = 5 
                    },
                };
            }

            return new HomeViewModel
            {
                Favoritos = restaurantesDestacados,
                Reseñas = new List<Testimonial>
                {
                    new Testimonial 
                    { 
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
                }
            };
        }
    }
}