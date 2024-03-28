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


        [HttpGet("[action]/{vilaId:int}")]
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
        /// دریافت یک جز ویلا
        /// </summary>
        /// <param name="detailId">ای دی جز </param>
        /// <returns></returns>
        [HttpGet("[action]/{detailId:int}", Name = "GetById")]   
        public IActionResult GetById(int detailId)
        {
            var detail = _detailService.GetById(detailId);
            if (detail == null) return StatusCode(404);
            var model = _mapper.Map<DetailDto>(detail);
            return StatusCode(200, model);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] DetailDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detail = _mapper.Map<Models.Detail>(model);
            if (_detailService.Create(detail))
            {
                return StatusCode(204);
            }
            ModelState.AddModelError("", "مشکل از سمت سرور میباشد .. لطفا مجددا تلاش کنید .");
            return StatusCode(500, ModelState);
        }

        [HttpPatch("[action]/{detailId:int}")]
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
        /// حذف جز ویلا
        /// </summary>
        /// <param name="detailId">کلید جز</param>
        /// <returns></returns>
        [HttpDelete("[action]/{detailId:int}")]
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
