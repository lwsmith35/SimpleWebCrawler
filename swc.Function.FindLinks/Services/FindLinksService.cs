using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using swc.Function.FindLinks.Interfaces;
using swc.Function.FindLinks.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace swc.Function.FindLinks.Services
{
    public class FindLinksService : IFindLinksService
    {
        private readonly ILogger<FindLinksService> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public FindLinksService(ILogger<FindLinksService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<(bool IsSuccessful, int LinksFollowedCount, string ErrorMessage)> ParseLinksFromPageAsync(PageRequestId requestPage)
        {
            int linksFollowed = 0;
            try
            {
                var pageStorageClient = httpClientFactory.CreateClient("PageStorage");
                var result = await pageStorageClient.GetAsync($"api/Pages/{requestPage.PageId}");
                if (! result.IsSuccessStatusCode)
                {
                    return (false, linksFollowed, result.ReasonPhrase);
                }
                var page = JsonConvert.DeserializeObject<Model.Page>(await result.Content.ReadAsStringAsync());

                var fetchUrlClient = httpClientFactory.CreateClient("FetchPage");

                // if domain link matches ship it off to FetchPage
                foreach (var (href, label) in ParseLinksFromContent(page.RawContent))
                {
                    // Need Requirement here, I'm assuming we save it even if we cannot Follow the link due to format issues
                    //_ = pageStorageClient.PostAsync($"api/Pages/{page.Id}/link");

                    if (Uri.TryCreate(href, UriKind.Absolute, out var uri))
                    {
                        if (uri.Host.Equals(page.Domain, StringComparison.InvariantCultureIgnoreCase))
                        {
                            logger?.LogInformation($"Crawling to Page -{page.Domain}/{page.ResourceLocation}-");
                            using var stringContent = new StringContent(JsonConvert.SerializeObject(new { url = href }), Encoding.UTF8, "application/json");
                            _ = fetchUrlClient.PostAsync("api/ProcessUrl", stringContent);
                            linksFollowed++;
                        }
                    }
                }

                return (true, linksFollowed, null);
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, linksFollowed, e.Message);
            }
        }

        private IEnumerable<(string href, string label)> ParseLinksFromContent(string htmlContent)
        {
            IList<(string href, string label)> links = new List<(string href, string label)>();

            if (!string.IsNullOrEmpty(htmlContent))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                var linkNodes = htmlDoc.DocumentNode.SelectNodes("//a");

                foreach( var node in linkNodes)
                {
                    logger?.LogTrace($"FoundLink: {node}");
                    links.Add((node.Attributes["href"].Value, node.InnerHtml));
                }
            }

            return links;
        }
    }
}
