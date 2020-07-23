using Binit.Framework;
using Binit.Framework.Helpers;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using WebApp.Middleware;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private ExceptionHandlerMiddleware exceptionHandlerMiddleware { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Localization/Resources");
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });

            // This is required to be able to use Razor components on MVC
            services.AddServerSideBlazor();

            // Register all services.
            new ServiceInjection(services, Configuration).Initialize();

            services.AddControllersWithViews()
                // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
                // Tracking code: 3.0.0-issue1
                .AddNewtonsoftJson()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResources));
                })
                .AddViewLocalization();

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
            });

            // Get an instance of ExceptionHandlerMiddleware to use on Configure().
            exceptionHandlerMiddleware = services.BuildServiceProvider().GetService<ExceptionHandlerMiddleware>();

            // Sets the current build version
            Versioning.UpdateVersion(Configuration);

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
                // Catch exceptions and build response accordingly.
                app.UseExceptionHandler(exceptionHandlerMiddleware.BuildExceptionResponse);
                app.UseStatusCodePages(exceptionHandlerMiddleware.BuildExceptionResponse);

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Configure localization
            var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("es") };
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(localizationOptions);
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            // Custom middleware that checks if user has access to the current realm.
            app.UseRealmAuthorization();

            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
