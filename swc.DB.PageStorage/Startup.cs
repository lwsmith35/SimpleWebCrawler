using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using swc.DB.PageStorage.DataCommand;
using swc.DB.PageStorage.DataQuery;
using swc.DB.PageStorage.Interfaces;
using swc.DB.PageStorage.Repository;

namespace swc.DB.PageStorage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext to use In memory DB, would need to change this out to a persistant storage.
            services.AddDbContext<PageDbContext>(
                opts =>
                {
                    opts.UseInMemoryDatabase("Pages");
                }
            );

            // Scope in our Page Data Collector and Data Provider
            services.AddScoped<IPageCommand, PageCommand>();
            services.AddScoped<IPageProvider, PageProvider>();

            // Want to know about AutoMapper 
            // See https://docs.automapper.org/en/latest/Getting-started.html
            // See https://docs.automapper.org/en/latest/Dependency-injection.html#asp-net-core
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
