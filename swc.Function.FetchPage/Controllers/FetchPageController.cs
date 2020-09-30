using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using swc.Function.FetchPage.Interfaces;

namespace swc.Function.FetchPage.Controllers
{
    [ApiController]
    [Route("[api/processUrl]")]
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
        public async Task<IActionResult> processUrl(string url)
        {
            var result = await processUrlService.processUrl(url);
            if (result.isSuccess)
            {
                return Ok(result.corelationId);
            }
            return BadRequest();
        }
    }
}
