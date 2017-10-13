using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multitenancy.Extensions;
using ThemesSample.Web.Multitenancy;

namespace ThemesSample.Web
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
            services.AddOptions();
            services.Configure<MultitenancyOptions>(Configuration.GetSection("Multitenancy"));

            services.AddMultitenancy<Theme, ThemeResolver>();

            services.AddMvc()
                .WithRazorPagesRoot("/Pages");

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseMultitenancy<Theme>();
            app.UseMvc();
        }
    }
}
