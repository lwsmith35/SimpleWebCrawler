using Microsoft.Extensions.Logging;
using swc.Function.FetchPage.Interfaces;
using swc.Function.FetchPage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Services
{
    public class FetchPageService : IFetchPageService
    {
        private readonly ILogger<FetchPageService> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public FetchPageService(ILogger<FetchPageService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }
        
        public async Task<(bool isSuccess, PageContent, string ErrorMsg)> FetchTargetContentAsync(string url)
        {
            try
            {
                // Fetch Headers for URL 
                var httpClient = httpClientFactory.CreateClient("ZeroConfigClient");
                var response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // @TODO: Introduce external mapper for Model
                    return (true,
                            new PageContent()
                            {
                                SourceURL = url,
                                RawContent = response.Content.ToString()
                            },
                            null);

                }
                return (false, null, response.ReasonPhrase);
            }
            catch(Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

        public async Task<(bool isSuccess, PageHeaders, string ErrorMsg)> FetchTargetHeadersAsync(string url)
        {
            try
            {
                // Fetch Headers for URL 
                var httpClient = httpClientFactory.CreateClient("ZeroConfigClient");
                var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                if (response.IsSuccessStatusCode)
                {
                    // @TODO: Introduce external mapper for Model


                }
                return (false, null, response.ReasonPhrase);
            }
            catch(Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
