using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.CustomerModels;
using Vila.WebApi.Services.Customer;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(_customerService.ExistMobile(model.Mobile))
            {
                ModelState.AddModelError("model.Mobile", "شماره موبایل تکراری می باشد!");
                return BadRequest(ModelState);
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
    }
}
