using Binit.Framework.Constants.Authentication;
using Binit.Framework.Helpers;
using Binit.Framework.Helpers.Configuration;
using Binit.Framework.Interfaces.Configuration;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.Email;
using Domain.Logic;
using Domain.Logic.BusinessLogic;
using Domain.Logic.Interfaces;
using Domain.Logic.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Middleware;
using WebAPITools;

namespace WebAPI
{
    /// <summary>
    /// WebAPI custom service injection class.
    /// Inherits from ServiceInjectionBase to have access to the Data Acces Layer registered services.
    /// </summary>
    public class ServiceInjection : ServiceInjectionBase
    {
        public ServiceInjection(IServiceCollection services, IConfiguration configuration)
            : base(services, configuration)
        {
        }

        /// <summary>
        /// Registers all services from the Domain.Logic Layer required by this specific Project.
        /// </summary>
        public override void RegisterServices()
        {
            this.Services.AddScoped<ExceptionHandlerMiddleware>();

            // Operation context
            this.Services.AddScoped<IOperationContext, WebOperationContext>();

            // Required entity services.
            this.Services.AddScoped<IAccountService, AccountService>();
            this.Services.AddScoped<IProductService, ProductService>();
            this.Services.AddScoped<ITenantService, TenantService>();
            this.Services.AddScoped<ICategoryService, CategoryService>();
            this.Services.AddScoped<IHolidayService, HolidayService>();

            // Business Logic
            this.Services.AddScoped<IHolidayBusinessLogic, HolidayBusinessLogic>();

            this.Services.AddScoped<TokenManager>();

            this.Services.AddApplicationInsightsTelemetry();

            // Configuration
            this.Services.AddScoped<IRealmConfiguration, RealmConfiguration>();


            #region EmailService

            //TODO:  
            //Use AppSettings / User Secret

            var smtpConfig = new SmtpConfiguration().Bind(Configuration);

            //TODO: If not configured there must be an exception telling so
            this.Services.AddFluentEmail(smtpConfig.Address)
                .AddRazorRenderer()
                .AddSmtpSender(smtpConfig.GenerateSmtpClient());

            this.Services.AddTransient<IEmailSender, EmailSender>();

            #endregion
            #region ExternalProviders
            this.Services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    OAuthOptions options = Configuration.GetAuthenticationSection<OAuthOptions>(SocialLoginConstants.Google);
                    googleOptions.ClientId = options.ClientId;
                    googleOptions.ClientSecret = options.ClientSecret;
                })
                .AddFacebook(facebookOptions =>
                {
                    OAuthOptions options = Configuration.GetAuthenticationSection<OAuthOptions>(SocialLoginConstants.Facebook);
                    facebookOptions.ClientId = options.ClientId;
                    facebookOptions.ClientSecret = options.ClientSecret;
                })
                .AddTwitter(twitterOptions =>
                {
                    OAuthOptions options = Configuration.GetAuthenticationSection<OAuthOptions>(SocialLoginConstants.Twitter);
                    twitterOptions.ConsumerKey = options.ClientId;
                    twitterOptions.ConsumerSecret = options.ClientSecret;
                });
            #endregion
        }
    }
}