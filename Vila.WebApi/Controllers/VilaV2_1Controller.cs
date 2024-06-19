using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vila.WebApi.Paging;
using Vila.WebApi.Services.Vila;

namespace Vila.WebApi.Controllers
{
    [Route("api/v{version:ApiVersion}/Vila")]
    [ApiVersion("2.1")]
    [ApiController]
    public class VilaV2_1Controller : ControllerBase
    {
        private readonly IVilaService _vilaService;
        public VilaV2_1Controller(IVilaService vilaService)
        {
            _vilaService = vilaService;
        }
        /// <summary>
        /// جست وجوی ویلا
        /// </summary>
        /// <param name="pageId">صفحه چندم ؟</param>
        /// <param name="filter">متن جست وجو</param>
        /// <param name="take">تعداد نمایش</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(VilaAdminPaging))]
        [ProducesResponseType(400)]
        [Authorize(Roles ="admin")]
        public IActionResult Search(int pageId = 1, string? filter = "", int take = 2)
        {
            if (pageId < 1 || take < 1) return BadRequest();
            var model = _vilaService.SearchVilaAdmin(pageId, filter, take);
            return Ok(model);
        }
    }
}
