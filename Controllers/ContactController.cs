<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
=======
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
using DondeComemos.Models;
using DondeComemos.Data;

namespace DondeComemos.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ApplicationDbContext _context;
        
        public ContactController(ILogger<ContactController> logger, ApplicationDbContext context)
        {
<<<<<<< HEAD
            _logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));
=======
                _logger = logger;
                _context = context ?? throw new ArgumentNullException(nameof(context));
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
<<<<<<< HEAD
        public IActionResult Registrar(Contacto obj)
        {
            _logger.LogInformation("Registro de contacto recibido. {@Contacto}", obj);
            if (ModelState.IsValid)
            {
                _context.Contactos.Add(obj);
                _context.SaveChanges();
                ViewData["Mensaje"] = "Gracias por contactarnos. Pronto nos comunicaremos contigo.";
            }
            return View("Index");
        }
    }
}
=======
public IActionResult Registrar(Contacto obj)
{
    _logger.LogInformation("Registro de contacto recibido. {@Contacto}", obj);
    if (ModelState.IsValid)
    {
        _context.Contactos.Add(obj);
        _context.SaveChanges();
        ViewData["Mensaje"] = "Gracias por contactarnos. Pronto nos comunicaremos contigo.";
    }
    return View("Index");
}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
