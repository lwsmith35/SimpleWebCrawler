using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace swc.Function.FindLinks.Controllers
{
    [ApiController]
    [Route("api/ProcessStaticContent")]
    public class ProcessStaticContentController : ControllerBase
    {
        private readonly ILogger<ProcessStaticContentController> logger;

        public ProcessStaticContentController(ILogger<ProcessStaticContentController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProcessStaticContent([FromBody]string PageId)
        {
            logger?.LogInformation($"Processing Static Content for {PageId}");
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
