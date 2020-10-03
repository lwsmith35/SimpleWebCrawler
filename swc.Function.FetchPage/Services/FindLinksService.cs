using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using swc.Function.FetchPage.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Services
{
    public class FindLinksService : IFindLinksService
    {
        private readonly ILogger logger;
        private readonly IHttpClientFactory httpClientFactory;

        public FindLinksService(ILogger<FindLinksService> logger, IHttpClientFactory httpClientFactory )
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public Task<HttpResponseMessage> SendPageToLinkProcess(Guid PageId)
        {
            try
            {

                var content = JsonConvert.SerializeObject(new
                {
                    PageId
                });
                using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    var linksClient = httpClientFactory.CreateClient("FindLinks");
                    var result = linksClient.PostAsync("api/FindLinks", stringContent);
                    return result;
                };
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return null;
            }
        }
    }
}
