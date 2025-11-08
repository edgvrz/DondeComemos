using Microsoft.AspNetCore.Mvc;
using DondeComemos.Models;
using DondeComemos.Services;

namespace DondeComemos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            var model = _homeService.GetHomeData();
            return View(model);
        }
<<<<<<< HEAD

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
=======
    }
}
>>>>>>> f90b87d81de3ce8c6b022ece9f01afa7f99a0eb7
