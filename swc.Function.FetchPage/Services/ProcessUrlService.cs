using Microsoft.Extensions.Logging;
using swc.Function.FetchPage.Interfaces;
using swc.Function.FetchPage.Model;
using System;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Services
{
    public class ProcessUrlService : IProcessUrlService
    {
        private readonly ILogger<ProcessUrlService> logger;
        private readonly IFetchPageService fetchPageService;
        private readonly ISavePageService savePageService;
        private readonly IFindLinksService findLinksService;
        private readonly IProcessStaticContentService staticContentService;

        public ProcessUrlService(ILogger<ProcessUrlService> logger, IFetchPageService fetchPageService, ISavePageService savePageService, IFindLinksService findLinksService, IProcessStaticContentService staticContentService)
        {
            this.logger = logger;
            this.fetchPageService = fetchPageService;
            this.savePageService = savePageService;
            this.findLinksService = findLinksService;
            this.staticContentService = staticContentService;
        }


        public async Task<(bool IsSuccess, CreatedPage Page, string ErrorMessage)> ProcessUrl(ProcessUrl request)
        {
            try
            {
                // Fetch Headers
                var requestHeaders = await fetchPageService.FetchTargetHeadersAsync(request.Url);
                if (requestHeaders.IsSuccess)
                {
                    // Validate Headers 
                    /*
                     * Use this area to check headers 
                     *  If possible look to optimize not refreshing a page if no changes were made, etc.
                     *  
                     *  For right now this serves as a light weight check before we try to download a full page
                     */
                }
                else
                { 
                    // If no headers were found the site must not be valid or 
                    return (false, null, "Unable to fetch site Headers");
                }

                // Fetch Content
                var requestContent = await fetchPageService.FetchTargetContentAsync(request.Url);
                if (requestContent.IsSuccess)
                {
                    // Validate Content

                    // Save Content
                    var (IsSuccess, newPage, ErrorMessage) = await savePageService.SavePage(requestContent.PageContent.SourceURL, requestContent.PageContent.RawContent);
                    if (IsSuccess)
                    {
                        // Kick off Orchestration Request OR Embed Orchastration Here
                        // Skipping awaits here as we should be processing contect asyncronously
                        _ = findLinksService.SendPageToLinkProcess(newPage.Id);
                        _ = staticContentService.SendPageToStaticContentProcess(newPage.Id);

                        return (true, newPage, null);
                    }

                    return (false, null, ErrorMessage);
                }
                return (false, null, "Unable to fetch site content");
            }
            catch( Exception e )
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
