using Microsoft.AspNetCore.Mvc;

namespace Vila.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }
}
