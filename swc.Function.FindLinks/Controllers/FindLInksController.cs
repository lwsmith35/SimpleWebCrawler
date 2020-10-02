using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace swc.Function.FindLinks.Controllers
{
    [ApiController]
    [Route("[api/FindLinks")]
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
        public async Task<IActionResult> ExtractLinksFromPage([FromBody]string PageId)
        {
            logger?.LogInformation($"Processing Links for {PageId}");
            if (false)
            {
                return BadRequest("Unable to parse URI");
            }

            return Accepted();
            //var pageResult = await pageCollector.SavePageAsync(newPage);
            // return CreatedAtAction(nameof(GetPageById), new { id = pageResult.Id }, pageResult);
        }
    }
}
