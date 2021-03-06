using BetterOwner.Services;
using BetterOwner.Services.Database;
using BetterOwner.Services.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BetterOwner
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
            services.AddMvc(o => o.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
            services.AddControllersWithViews().AddNewtonsoftJson();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            var defaultConnection = Configuration["DefaultConnection"];
            services.AddDbContextPool<ApplicationDbContext>(o => o.UseSqlServer(Configuration["DefaultConnection"]));
            services.ConfigureAppServices(Configuration);

            services.AddIdentity<User, Role>(o =>
                {
                    o.User.RequireUniqueEmail = true;
                    o.User.AllowedUserNameCharacters = null;
                })
                .AddDefaultUI()
                .AddUserManager<UserManager>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAntiforgery(o => o.HeaderName = "X-XSRF-TOKEN");

            services.AddAuthentication().AddYeluCasSso(o =>
            {
                o.YeluCasSsoEndpoint = Configuration["YeluCasSsoEndpoint"];
                //o.Events.OnCreatingClaims = UserManager.OnCreatingClaims;
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseAntiforgeryToken();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
