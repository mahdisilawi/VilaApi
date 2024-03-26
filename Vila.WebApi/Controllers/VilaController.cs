using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Dtos;
using Vila.WebApi.Models;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Vila")]
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

        [HttpGet]
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

        [HttpGet("[action]/{vilaId:int}")]
        public IActionResult GetDetails(int vilaId)
        {
            var vilaDetail = _vilaService.GetById(vilaId);
            if (vilaDetail == null) return NotFound();
            var model = _mapper.Map<VilaDto>(vilaDetail);
            return Ok(model);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] VilaDto model)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            var vila = _mapper.Map<Models.Vila>(model);
            if (_vilaService.Create(vila))
            {
                return StatusCode(201);
            }
            ModelState.AddModelError(string.Empty, "مشکل از سمت سرور می باشد...");

            
            return StatusCode(500,ModelState);
        }

    }
}
