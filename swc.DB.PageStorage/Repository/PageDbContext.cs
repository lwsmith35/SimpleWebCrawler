using Microsoft.EntityFrameworkCore;

namespace swc.DB.PageStorage.Repository
{
    public class PageDbContext: DbContext
    {
        public DbSet<Page> Page { get; set; }
        public PageDbContext(DbContextOptions opts) : base(opts) { }
    }
}
