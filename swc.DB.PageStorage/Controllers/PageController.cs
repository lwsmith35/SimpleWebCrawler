using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using swc.DB.PageStorage.Interfaces;

namespace swc.DB.PageStorage.Controllers
{
    [ApiController]
    [Route("api/pages")]
    public class PageController : Controller
    {
        private readonly ILogger<PageController> logger;
        private readonly IPageProvider pageProvider;
        private readonly IPageCollector pageCollector;

        public PageController(ILogger<PageController> logger, IPageProvider pageProvider, IPageCollector pageCollector)
        {
            this.logger = logger;
            this.pageProvider = pageProvider;
            this.pageCollector = pageCollector;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPageByDomain([FromQuery] string domain)
        {
            if (!string.IsNullOrEmpty(domain))
            {
                var (IsSuccess, pages, ErrorMsg) = await pageProvider.GetPagesInDomainAsync(domain);
                if (IsSuccess)
                {
                    return Ok(JsonConvert.SerializeObject(pages, Formatting.Indented));
                }
                return NotFound(ErrorMsg);
            }
            else
            {
                var (IsSuccess, pages, ErrorMsg) = await pageProvider.GetPagesAsync();
                if (IsSuccess)
                {
                    return Ok(JsonConvert.SerializeObject(pages, Formatting.Indented));
                }
                return NotFound(ErrorMsg);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPageById([FromRoute] Guid id)
        {
            var (IsSuccess, page, ErrorMsg) = await pageProvider.GetPageByIdAsync(id);
            if (IsSuccess)
            {
                return Ok(page);
            }
            return NotFound();
        }


        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SavePage([FromBody]Model.NewPage newPage)
        {
            if (! Uri.TryCreate(newPage.ResourceUrl, UriKind.RelativeOrAbsolute, out var uri) )
            {
                return BadRequest("Unable to parse URI");
            }

            var pageResult = await pageCollector.SavePageAsync(newPage);

            return CreatedAtAction(nameof(GetPageById), new { id = pageResult.Id }, pageResult);
        }
    }
}
