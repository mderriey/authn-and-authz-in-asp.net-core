using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAndAuthorisation
{
    public class DemoRequirement : IAuthorizationRequirement
    {
    }

    public class DemoRequirementHandler : AuthorizationHandler<DemoRequirement>
    {
        private readonly ISystemClock _clock;

        public DemoRequirementHandler(ISystemClock clock)
        {
            _clock = clock;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DemoRequirement requirement)
        {
            if (_clock.UtcNow.Second > 30)
            {
                context.Succeed(requirement);
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
                .AddCookie("Cookies");

            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("Manager", builder => builder
                        .RequireAuthenticatedUser()
                        .RequireRole("manager"));

                    options.AddPolicy("Admin", builder => builder
                        .RequireAuthenticatedUser()
                        .RequireRole("admin")
                        .AddRequirements(new DemoRequirement()));
                });

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
