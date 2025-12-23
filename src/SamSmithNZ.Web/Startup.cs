using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SamSmithNZ.Web.Services;
using SamSmithNZ.Web.Services.Interfaces;

namespace SamSmithNZ.Web
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
            services.AddControllersWithViews();
            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = Configuration["APPINSIGHTS_CONNECTIONSTRING"];
            });

            //Add DI for the service api client 
            services.AddScoped<IFooFightersServiceApiClient, FooFightersServiceApiClient>();
            services.AddScoped<IGuitarTabServiceApiClient, GuitarTabServiceApiClient>();
            services.AddScoped<IWorldCupServiceApiClient, WorldCupServiceApiClient>();
            services.AddScoped<ISteamServiceApiClient, SteamServiceApiClient>();
            services.AddScoped<IITunesServiceApiClient, ITunesServiceApiClient>();
            services.AddScoped<IMandMCounterServiceApiClient, MandMCounterServiceApiClient>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                ////Capture any spam calls to wp-login.php
                //endpoints.MapControllerRoute(name: "wpspam",
                //    pattern: "wp-login.php",
                //    defaults: new { controller = "Spam", action = "Index" });

                ////Capture any old calls to IntFootball and redirect to worldcup
                //endpoints.MapControllerRoute(name: "intfootball",
                //    pattern: "intfootball/{*page}",
                //    defaults: new { controller = "WorldCup", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
