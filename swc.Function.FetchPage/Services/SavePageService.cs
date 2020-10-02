using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using swc.Function.FetchPage.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Services
{
    public class SavePageService : ISavePageService
    {
        private readonly ILogger<SavePageService> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public SavePageService(ILogger<SavePageService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccess, string PageId, string ErrorMessage)> SavePage(string resourceUrl, string pageContent)
        {
            try
            {

                var content = JsonConvert.SerializeObject(new
                {
                    ResourceUrl = resourceUrl,
                    RawContent = pageContent
                });
                using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    var pageDbClient = httpClientFactory.CreateClient("PageStorage");
                    var result = await pageDbClient.PostAsync("api/pages", stringContent);

                    if (result.IsSuccessStatusCode)
                    {
                        return (true, "", null);
                    }

                    return (false, null, $"Failed to save page: {result.ReasonPhrase}");
                };
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
