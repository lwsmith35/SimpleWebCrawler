using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using swc.Function.FindLinks.Model;

namespace swc.Function.FindLinks.Controllers
{
    [ApiController]
    [Route("api/FindLinks")]
    public class FindLInksController : ControllerBase
    {
        private readonly ILogger<FindLInksController> logger;

        public FindLInksController(ILogger<FindLInksController> logger)
        {
            this.logger = logger;
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

            await Task.Delay(100);
            return Accepted();
            //var pageResult = await pageCollector.SavePageAsync(newPage);
            // return CreatedAtAction(nameof(GetPageById), new { id = pageResult.Id }, pageResult);
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
