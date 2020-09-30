using Microsoft.Extensions.Logging;
using swc.Function.FetchPage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Services
{
    public class ProcessUrlService : IProcessUrlService
    {
        private readonly ILogger<ProcessUrlService> logger;
        private readonly IFetchPageService fetchPageService;

        public ProcessUrlService(ILogger<ProcessUrlService> logger, IFetchPageService fetchPageService)
        {
            this.logger = logger;
            this.fetchPageService = fetchPageService;
        }


        public async Task<(bool isSuccess, string corelationId)> processUrl(string Url)
        {
            try
            {
                // Fetch Headers
                var requestHeaders = await fetchPageService.FetchTargetHeadersAsync(Url);
                if (requestHeaders.isSuccess)
                {
                    // Validate Headers 

                }
                else
                { 
                    // If no headers were found the site must not be valid or 
                    return (false, null);
                }

                // Fetch Content
                var requestContent = await fetchPageService.FetchTargetHeadersAsync(Url);
                if (requestContent.isSuccess)
                {
                    // Validate Content
                    // Save Content

                    // Kick off Orchestration Request OR Embed Orchastration Here

                    return (true, Guid.NewGuid().ToString());
                }
                return (false, null);
            }
            catch( Exception e )
            {
                logger?.LogError(e.ToString());
                return (false, null);
            }
        }
    }
}
