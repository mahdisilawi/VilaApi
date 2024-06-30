using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Vila.Web.Models;

namespace Vila.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
        public IActionResult Error()
        {
            return View();
        }
    }
}
