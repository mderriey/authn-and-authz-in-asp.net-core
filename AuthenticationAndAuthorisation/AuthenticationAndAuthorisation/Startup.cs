using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAndAuthorisation
{
    public class AppCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ISystemClock _clock;

        public AppCookieAuthenticationEvents(ISystemClock clock)
        {
            _clock = clock;
        }

        public override Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            if (_clock.UtcNow.Second > 30)
            {
                context.RejectPrincipal();
            }

            return Task.CompletedTask;
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication("Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/AccessDenied");

                    options.Cookie.Expiration = TimeSpan.FromDays(10);
                    options.SlidingExpiration = true;

                    options.EventsType = typeof(AppCookieAuthenticationEvents);
                });

            services.AddTransient<AppCookieAuthenticationEvents>();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
