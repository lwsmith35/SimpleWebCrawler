using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using swc.DB.PageStorage.Interfaces;
using swc.DB.PageStorage.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace swc.DB.PageStorage.DataCommand
{
    public class PageCollector : IPageCollector
    {
        private readonly ILogger logger;
        private readonly IMapper autoMap;
        private readonly PageDbContext dbContext;

        public PageCollector(ILogger<PageCollector> logger, IMapper autoMap, PageDbContext dbContext )
        {
            this.logger = logger;
            this.autoMap = autoMap;
            this.dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, Guid? Id, string ErrorMessage)> SavePageAsync(Model.NewPage page)
        {
            try
            {
                // Test Url
                var targetUri = new Uri(page.ResourceUrl);
                var domain = targetUri.Host;
                var resource = targetUri.AbsolutePath;

                // Validate if we already have this page
                var existingPage = await dbContext.Page.Where(p => p.Domain == domain && p.ResourceLocation == resource).Select(p=> p.Id).FirstOrDefaultAsync();
                if (existingPage != Guid.Empty)
                {
                    return (true, existingPage, null);
                };

                // Map new Page
                var newPage = autoMap.Map<Model.NewPage, Repository.Page>(page);
                newPage.Id = Guid.NewGuid();
                newPage.Domain = domain;
                newPage.ResourceLocation = resource;

                // Save Page
                dbContext.Page.Add(newPage);
                await dbContext.SaveChangesAsync();

                return (true, newPage.Id, null);
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
