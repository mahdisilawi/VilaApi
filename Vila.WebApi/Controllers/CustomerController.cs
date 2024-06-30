using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Dtos;
using Vila.WebApi.Services.Customer;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// ثبت نام در سایت
        /// </summary>
        /// <param name="model"> موبایل و کلمه عبور</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(_customerService.ExistMobile(model.Mobile))
            {
                
                return BadRequest(new {error  = "شماره موبایل تکراری می باشد!" });
            }

            if (_customerService.Register(model))
            {
                return StatusCode(201);
            }
            else
            {
                ModelState.AddModelError("", "خطای سیستمی ... لطفا مجددا اقدام نمایید !!!");
                return StatusCode(500, ModelState);
            }
        }



        /// <summary>
        /// ورود به سایت
        /// </summary>
        /// <param name="model"> موبایل و کلمه عبور</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(200,Type = typeof(LoginResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] RegisterModel login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_customerService.PasswordIsCorrect(login.Mobile,login.Password))
            {
                return BadRequest(new {error = "کاربری یافت نشد!" });
            }

            var customer = _customerService.Login(login.Mobile,login.Password);
            if (customer == null) return NotFound();

            return Ok(customer);
        }
    }
}
