using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using swc.Function.FetchPage.Interfaces;
using swc.Function.FetchPage.Services;

namespace swc.Function.FetchPage
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
            services.AddScoped<ISavePageService, SavePageService>();
            services.AddScoped<IFetchPageService, FetchPageService>();
            services.AddScoped<IFindLinksService, FindLinksService>();
            services.AddScoped<IProcessStaticContentService, ProcessStaticContentService>();
            services.AddScoped<IProcessUrlService, ProcessUrlService>();

            // Generic Http Client for fetching requested site
            services.AddHttpClient("ZeroConfigClient", config => { });

            // Related Services, Ideally only Storage would be here and orchastraion with parsing content would not be a dependency
            services.AddHttpClient("PageStorage",           config => { config.BaseAddress = new Uri(Configuration["ServiceList:PageStorage"]); });
            services.AddHttpClient("FindLinks",             config => { config.BaseAddress = new Uri(Configuration["ServiceList:FindLinks"]); });
            services.AddHttpClient("ProcessStaticContent",  config => { config.BaseAddress = new Uri(Configuration["ServiceList:ProcessStaticContent"]); });

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
