using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using swc.Function.FetchPage.Interfaces;
using swc.Function.FetchPage.Model;

namespace swc.Function.FetchPage.Controllers
{
    [ApiController]
    [Route("api/ProcessUrl")]
    public class FetchPageController : ControllerBase
    {
        private readonly ILogger<FetchPageController> logger;
        private readonly IProcessUrlService processUrlService;

        public FetchPageController(ILogger<FetchPageController> logger, IProcessUrlService processUrlService)
        {
            this.logger = logger;
            this.processUrlService = processUrlService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessUrl([FromBody] ProcessUrl url )
        {
            var (isSuccess, Page, ErrorMessage) = await processUrlService.ProcessUrl(url);
            if (isSuccess)
            {
                return Accepted(Page);
            }
            return BadRequest(ErrorMessage??$"Unable to process requested url {url}");
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
