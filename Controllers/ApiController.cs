using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DondeComemos.Data;

namespace DondeComemos.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("restaurantes/mapa")]
        public async Task<IActionResult> GetRestaurantesParaMapa()
        {
            var restaurantes = await _context.Restaurantes
                .Where(r => r.Latitud != null && r.Longitud != null)
                .Select(r => new
                {
                    id = r.Id,
                    nombre = r.Nombre,
                    descripcion = r.Descripcion,
                    direccion = r.Direccion,
                    imagenUrl = r.ImagenUrl,
                    latitud = r.Latitud,
                    longitud = r.Longitud,
                    rating = r.Rating,
                    tipoCocina = r.TipoCocina,
                    rangoPrecios = r.RangoPrecios
                })
                .ToListAsync();

            return Ok(restaurantes);
        }

        [HttpGet("restaurantes/cercanos")]
        public async Task<IActionResult> GetRestaurantesCercanos(double lat, double lng, double radioKm = 5)
        {
            var restaurantes = await _context.Restaurantes
                .Where(r => r.Latitud != null && r.Longitud != null)
                .ToListAsync();

            // Calcular distancia usando fórmula de Haversine
            var restaurantesCercanos = restaurantes
                .Select(r => new
                {
                    id = r.Id,
                    nombre = r.Nombre,
                    descripcion = r.Descripcion,
                    direccion = r.Direccion,
                    imagenUrl = r.ImagenUrl,
                    latitud = r.Latitud,
                    longitud = r.Longitud,
                    rating = r.Rating,
                    tipoCocina = r.TipoCocina,
                    rangoPrecios = r.RangoPrecios,
                    distanciaKm = CalcularDistancia(lat, lng, r.Latitud!.Value, r.Longitud!.Value)
                })
                .Where(r => r.distanciaKm <= radioKm)
                .OrderBy(r => r.distanciaKm)
                .ToList();

            return Ok(restaurantesCercanos);
        }

        private double CalcularDistancia(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Radio de la Tierra en kilómetros

            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}