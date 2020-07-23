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
using WebAPI.Middleware;
using WebAPI.WebAPITools;

namespace WebAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddLocalization(options => options.ResourcesPath = "Localization/Resources");

            services.AddControllers()
                // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
                // Tracking code: 3.0.0-issue1
                .AddNewtonsoftJson()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResources));
                });

            services.AddSwaggerDocumentation();

            // Inject all services.
            new ServiceInjection(services, Configuration).Initialize();

            // Get an instance of ExceptionHandlerMiddleware to use on Configure().
            exceptionHandlerMiddleware = services.BuildServiceProvider().GetService<ExceptionHandlerMiddleware>();

            // Sets the current build version
            Versioning.UpdateVersion(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            // Catch exceptions and build response accordingly.
            app.UseExceptionHandler(exceptionHandlerMiddleware.BuildExceptionResponse);
            app.UseStatusCodePages(exceptionHandlerMiddleware.BuildExceptionResponse);

            if (!env.IsDevelopment())
            {
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

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // Custom middleware that checks if user has access to the current realm.
            app.UseRealmAuthorization();

            app.UseElmah();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();
        }
    }
}
