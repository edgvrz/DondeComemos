using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DondeComemos.Data;

namespace DondeComemos.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EstadisticasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Calcular los cutoffs fuera de la expresión LINQ para evitar que EF intente traducir DateTime.Now.*
            var cutoff6Months = DateTime.Now.AddMonths(-6);
            var cutoff30Days = DateTime.Now.AddDays(-30);

            var totalRestaurantesTask = _context.Restaurantes.CountAsync();
            var totalProductosTask = _context.Productos.CountAsync();
            var totalResenasTask = _context.Resenas.CountAsync();
            var totalUsuariosTask = _context.UserProfiles.CountAsync();
            var totalContactosTask = _context.Contactos.CountAsync();
            var totalSugerenciasTask = _context.Sugerencias.CountAsync();

            var restaurantesDestacadosTask = _context.Restaurantes.Where(r => r.Destacado).CountAsync();

            var promedioCalificacionTask = _context.Restaurantes
                .AverageAsync(r => (double?)r.Rating);

            var restaurantesMejorCalificadosTask = _context.Restaurantes
                .OrderByDescending(r => r.Rating)
                .Take(5)
                .Select(r => new { r.Nombre, r.Rating, TotalResenas = r.Resenas.Count })
                .ToListAsync();

            var resenasPorMesRawTask = _context.Resenas
                .Where(r => r.FechaCreacion >= cutoff6Months)
                .GroupBy(r => new { Year = r.FechaCreacion.Year, Month = r.FechaCreacion.Month })
                .Select(g => new { Year = g.Key.Year, Month = g.Key.Month, Total = g.Count() })
                .ToListAsync();

            var productosPorCategoriaTask = _context.Productos
                .GroupBy(p => p.Categoria)
                .Select(g => new { Categoria = g.Key, Total = g.Count() })
                .OrderByDescending(x => x.Total)
                .ToListAsync();

            var restaurantesPorTipoCocinaTask = _context.Restaurantes
                .Where(r => !string.IsNullOrEmpty(r.TipoCocina))
                .GroupBy(r => r.TipoCocina)
                .Select(g => new { TipoCocina = g.Key, Total = g.Count() })
                .OrderByDescending(x => x.Total)
                .ToListAsync();

            var ultimasResenasTask = _context.Resenas
                .Include(r => r.Restaurante)
                .OrderByDescending(r => r.FechaCreacion)
                .Take(10)
                .ToListAsync();

            var restaurantesSinResenasTask = _context.Restaurantes
                .Where(r => !r.Resenas.Any())
                .CountAsync();

            var usuariosActivosTask = _context.UserProfiles
                .Where(u => u.UltimaActividad >= cutoff30Days)
                .CountAsync();

            await Task.WhenAll(
                totalRestaurantesTask, totalProductosTask, totalResenasTask, totalUsuariosTask,
                totalContactosTask, totalSugerenciasTask, restaurantesDestacadosTask, promedioCalificacionTask,
                restaurantesMejorCalificadosTask, resenasPorMesRawTask, productosPorCategoriaTask,
                restaurantesPorTipoCocinaTask, ultimasResenasTask, restaurantesSinResenasTask, usuariosActivosTask
            );

            var resenasPorMes = resenasPorMesRawTask.Result
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .Select(x => new
                {
                    Mes = $"{x.Year}-{x.Month:00}",
                    x.Total
                })
                .ToList();

            var estadisticas = new
            {
                TotalRestaurantes = totalRestaurantesTask.Result,
                TotalProductos = totalProductosTask.Result,
                TotalResenas = totalResenasTask.Result,
                TotalUsuarios = totalUsuariosTask.Result,
                TotalContactos = totalContactosTask.Result,
                TotalSugerencias = totalSugerenciasTask.Result,

                RestaurantesDestacados = restaurantesDestacadosTask.Result,

                PromedioCalificacion = promedioCalificacionTask.Result ?? 0,

                RestaurantesMejorCalificados = restaurantesMejorCalificadosTask.Result,

                ResenasPorMes = resenasPorMes,

                ProductosPorCategoria = productosPorCategoriaTask.Result,
                RestaurantesPorTipoCocina = restaurantesPorTipoCocinaTask.Result,

                UltimasResenas = ultimasResenasTask.Result,

                RestaurantesSinResenas = restaurantesSinResenasTask.Result,

                UsuariosActivos = usuariosActivosTask.Result
            };

            return View(estadisticas);
        }

        [HttpGet]
        public async Task<IActionResult> GetChartData(string type)
        {
            switch (type)
            {
                case "resenasPorMes":
                {
                    var cutoff12Months = DateTime.Now.AddMonths(-12);

                    var raw = await _context.Resenas
                        .Where(r => r.FechaCreacion >= cutoff12Months)
                        .GroupBy(r => new { Year = r.FechaCreacion.Year, Month = r.FechaCreacion.Month })
                        .Select(g => new { Year = g.Key.Year, Month = g.Key.Month, Total = g.Count() })
                        .ToListAsync();

                    var resenasPorMes = raw
                        .OrderBy(x => x.Year)
                        .ThenBy(x => x.Month)
                        .Select(x => new
                        {
                            Mes = $"{x.Month:00}/{x.Year}",
                            x.Total
                        })
                        .ToList();

                    return Json(resenasPorMes);
                }

                case "calificacionesDistribucion":
                {
                    var calificaciones = await _context.Resenas
                        .Select(r => r.Calificacion)
                        .ToListAsync();

                    var distribucion = calificaciones
                        .GroupBy(c => (int)Math.Floor(c)) 
                        .Select(g => new
                        {
                            Calificacion = g.Key,
                            Total = g.Count()
                        })
                        .OrderBy(x => x.Calificacion)
                        .ToList();

                    return Json(distribucion);
                }

                case "topRestaurantes":
                {
                    var topRestaurantes = await _context.Restaurantes
                        .Select(r => new
                        {
                            r.Nombre,
                            Rating = r.Rating,
                            TotalResenas = r.Resenas.Count
                        })
                        .OrderByDescending(x => x.Rating)
                        .ThenByDescending(x => x.TotalResenas)
                        .Take(10)
                        .ToListAsync();

                    return Json(topRestaurantes);
                }

                default:
                    return BadRequest();
            }
        }
    }
}
