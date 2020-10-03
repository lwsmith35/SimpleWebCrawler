using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using swc.Function.FindLinks.Interfaces;
using swc.Function.FindLinks.Model;

namespace swc.Function.FindLinks.Controllers
{
    [ApiController]
    [Route("api/FindLinks")]
    public class FindLInksController : ControllerBase
    {
        private readonly ILogger<FindLInksController> logger;
        private readonly IFindLinksService findLinks;

        public FindLInksController(ILogger<FindLInksController> logger, IFindLinksService findLinks)
        {
            this.logger = logger;
            this.findLinks = findLinks;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExtractLinksFromPage([FromBody]PageRequestId page)
        {
            logger?.LogInformation($"Processing Links for {page.PageId}");
            if (false)
            {
                return BadRequest("Unable to parse URI");
            }

            _ = await findLinks.ParseLinksFromPageAsync(page);
            return Accepted();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ApiAck()
        {
            await Task.Delay(1);
            return Ok($"{this.GetType().Name} is alive");
        }
    }
}
