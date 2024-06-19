using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Models;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    //[ApiVersion("1.0")]
    [ApiController]
    public class VilaController : ControllerBase
    {
        private readonly IVilaService _vilaService;
        private readonly IMapper _mapper;
        public VilaController(IVilaService vilaService, IMapper mapper)
        {
            _vilaService = vilaService;
            _mapper = mapper;

        }
        /// <summary>
        /// دریافت لیست ویلاها
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VilaDto>))]
        public IActionResult GetAll()
        {
            var list = _vilaService.GetAll();
            List<VilaDto> model = new();

            list.ForEach(x =>
            {
                model.Add(_mapper.Map<VilaDto>(x));
            });

            return Ok(model);
        }

        /// <summary>
        /// دریافت یک ویلاباآی دی 
        /// </summary>
        /// <param name="vilaId">کلید ویلا</param>
        /// <returns></returns>
        [HttpGet("[action]/{vilaId:int}", Name = "GetDetails")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VilaDto))]
        [ProducesResponseType(404)]
        [Authorize]

        public IActionResult GetDetails(int vilaId)
        {
            var vilaDetail = _vilaService.GetById(vilaId);
            if (vilaDetail == null) return NotFound();
            var model = _mapper.Map<VilaDto>(vilaDetail);
            return Ok(model);
        }

        /// <summary>
        /// ایجاد یک ویلا جدید
        /// </summary>
        /// <param name="model">اطلاعات ویلا</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(VilaDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "admin")]

        public IActionResult Create([FromBody] VilaDto model)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            var vila = _mapper.Map<Models.Vila>(model);
            if (_vilaService.Create(vila))
            {
                return CreatedAtRoute("GetDetails", new {vilaId = vila.VilaId},_mapper.Map<VilaDto>(vila));
            }
            ModelState.AddModelError(string.Empty, "مشکل از سمت سرور می باشد...");

            
            return StatusCode(500,ModelState);
        }

        /// <summary>
        /// ویرایش ویلا
        /// </summary>
        /// <param name="vilaId">کلید ویلا</param>
        /// <param name="model">اطلاعات ویلا</param>
        /// <returns></returns>
        [HttpPatch("[action]/{vilaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "admin")]

        public IActionResult Update(int vilaId,[FromBody] VilaDto model)
        {
            if (vilaId != model.VilaId)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vila = _mapper.Map<Models.Vila>(model);
            if (_vilaService.Update(vila))
            {
                return StatusCode(204);
            }
            ModelState.AddModelError(string.Empty, "مشکل از سمت سرور می باشد...");


            return StatusCode(500, ModelState);
        }

        /// <summary>
        /// حذف ویلا
        /// </summary>
        /// <param name="vilaId">کلید ویلا</param>
        /// <returns></returns>
        [HttpDelete("[action]/{vilaId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [Authorize(Roles ="admin")]
        public IActionResult Remove(int vilaId)
        {
            var vila = _vilaService.GetById(vilaId);
            if (vila == null)
                return NotFound();
            if (_vilaService.Delete(vila))
            {
                return NoContent();
            }
            ModelState.AddModelError(string.Empty, "مشکل از سمت سرور می باشد...");


            return StatusCode(500, ModelState);
        }

    }
}
