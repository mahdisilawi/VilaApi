using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Models;
using Vila.WebApi.Services.Detail;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    [Route("api/Detail")]
    [ApiController]
    public class DetailController : ControllerBase
    {
        private readonly IDetailService _detailService;
        private readonly IVilaService _vilaService;
        private readonly IMapper _mapper; 
        public DetailController(
            IVilaService vilaService,
            IDetailService detailService,
            IMapper mapper)
        {
            _detailService = detailService;
            _vilaService = vilaService;
            _mapper = mapper;
        }

        /// <summary>
        /// دریافت لیست ویژگی های یک ویلا
        /// </summary>
        /// <param name="vilaId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{vilaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DetailDto>))]
        public IActionResult GetAllVilaDetails(int vilaId)
        {
            var vila = _vilaService.GetById(vilaId);
            if (vila == null)
                return NotFound();

            var details = _detailService.GetAllVilaDetails(vilaId);
            List<DetailDto> model = new();

            details.ForEach(x =>
            {
                model.Add(_mapper.Map<DetailDto>(x));
            });
            return StatusCode(200, model);
        }

        /// <summary>
        /// دریافت یک ویژگی ویلا
        /// </summary>
        /// <param name="detailId">ای دی جز </param>
        /// <returns></returns>
        [HttpGet("[action]/{detailId:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetailDto))]
        [ProducesResponseType(404)]
        public IActionResult GetById(int detailId)
        {
            var detail = _detailService.GetById(detailId);
            if (detail == null) return StatusCode(404);
            var model = _mapper.Map<DetailDto>(detail);
            return StatusCode(200, model);
        }

        /// <summary>
        /// ایجاد ویژگی ویلا
        /// </summary>
        /// <param name="model">اطلاعات ویژگی</param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetailDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] DetailDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detail = _mapper.Map<Models.Detail>(model);
            if (_detailService.Create(detail))
            {
                var dtoDetail = _mapper.Map<DetailDto>(detail);
                return CreatedAtRoute("GetById", new { detailId = dtoDetail.DetailId }, dtoDetail);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }

        /// <summary>
        /// ویرایش یک ویژگی ویلا
        /// </summary>
        /// <param name="detailId">کلید ویژگی ویلا</param>
        /// <param name="model">اطلاعات ویژگی</param>
        /// <returns></returns>
        [HttpPatch("[action]/{detailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Update(int detailId, [FromBody] DetailDto model)
        {
            if (detailId != model.DetailId)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detail = _mapper.Map<Models.Detail>(model);
            if (_detailService.Update(detail))
            {
                //return NoContent();
                return StatusCode(204);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
        /// <summary>
        /// حذف ویژگی ویلا
        /// </summary>
        /// <param name="detailId">کلید ویژگی</param>
        /// <returns></returns>
        [HttpDelete("[action]/{detailId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public IActionResult Remove(int detailId)
        {
            var detail = _detailService.GetById(detailId);
            if (detail == null)
                return NotFound();

            if (_detailService.Delete(detail))
            {
                return NoContent();
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }
    }
}
