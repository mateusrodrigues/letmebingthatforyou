using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lmbtfy.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lmbtfy.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IDirectoryService, DirectoryService>();
            services.AddTransient<ImageRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "Search",
                    "",
                    new { controller = "Home", action = "Index" }
                );

                routes.MapRoute(
                    "OldSearch",
                    "{predicate}",
                    new { controller = "Home", action = "Index", predicate = "" }
                );

                routes.MapRoute(
                    "Default",
                    "{controller}/{action}",
                    new { action = "Index" }
                );
            });
        }
    }
}
