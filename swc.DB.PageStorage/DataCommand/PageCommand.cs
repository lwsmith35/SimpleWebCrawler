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
    public class PageCommand : IPageCommand
    {
        private readonly ILogger logger;
        private readonly IMapper autoMap;
        private readonly PageDbContext dbContext;

        public PageCommand(ILogger<PageCommand> logger, IMapper autoMap, PageDbContext dbContext )
        {
            this.logger = logger;
            this.autoMap = autoMap;
            this.dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeletePageByIdAsync(Guid PageId)
        { try {
                var entity = await dbContext.Page.FirstOrDefaultAsync(page => page.Id == PageId);
                if (entity != null && entity.Id != Guid.Empty)
                {
                    var result = dbContext.Page.Remove(entity);
                    _ = await dbContext.SaveChangesAsync();

                    return (true, null);
                }

                return (false, "Failed to delete specified Entity");
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, e.Message);
            }

        }

        public async Task<(bool IsSuccess, Guid? Id, bool PageExists, string ErrorMessage)> SavePageAsync(Model.NewPage page)
        {
            try
            {
                // Test Url
                var targetUri = new Uri(page.ResourceUrl);
                var domain = targetUri.Host;
                var resource = targetUri.AbsolutePath;

                // Validate if we already have this page
                var existingPage = await dbContext.Page.Where(p => p.Domain == domain.ToUpperInvariant() && p.ResourceLocation == resource).Select(p=> p.Id).FirstOrDefaultAsync(); if (existingPage != Guid.Empty)
                {
                    return (true, existingPage, true, null);
                };

                // Map new Page
                var newPage = autoMap.Map<Model.NewPage, Repository.Page>(page);
                newPage.Id = Guid.NewGuid();
                newPage.Domain = domain.ToUpperInvariant();
                newPage.ResourceLocation = resource;

                // Save Page
                dbContext.Page.Add(newPage);
                await dbContext.SaveChangesAsync();

                return (true, newPage.Id, false, null);
            }
            catch (Exception e)
            {
                logger?.LogError(e.ToString());
                return (false, null, false, e.Message);
            }
        }
    }
}
