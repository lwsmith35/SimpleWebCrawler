using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using swc.Function.FetchPage.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Services
{
    public class ProcessStaticContentService : IProcessStaticContentService
    {
        private readonly ILogger<ProcessStaticContentService> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public ProcessStaticContentService(ILogger<ProcessStaticContentService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public Task<HttpResponseMessage> SendPageToStaticContentProcess(string PageId)
        {
            try
            {
                var content = JsonConvert.SerializeObject(new
                {
                    PageId
                });

                using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    var staticContentClient = httpClientFactory.CreateClient("ProcessStaticContent");
                    var result = staticContentClient.PostAsync("api/ProcessStaticContent", stringContent);
                    return result;
                }
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return null;
            }
        }
    }
}
