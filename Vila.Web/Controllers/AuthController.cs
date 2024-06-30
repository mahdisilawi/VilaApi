using Microsoft.AspNetCore.Mvc;
using Vila.Web.Models.Customer;
using Vila.Web.Services.Customer;

namespace Vila.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public AuthController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if(!ModelState.IsValid) return View(model);
            var result = await _customerRepository.Register(model);
            if (result.Result)
            {
                TempData["success"] = true;
                return View();
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _customerRepository.Login(model);

            if (!result.Result.Result)
            {
                ModelState.AddModelError("", result.Result.Message);
                return View(model);
            }

            var customer = result.Customer;
            HttpContext.Session.SetString("JWTSecret", customer.JwtSecret);

            return Redirect("/");
        }
    }
}
