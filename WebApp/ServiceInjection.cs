using Binit.Framework.Constants.Authentication;
using Binit.Framework.Helpers;
using Binit.Framework.Helpers.Configuration;
using Binit.Framework.Interfaces.Configuration;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.Email;
using Domain.Entities.Model;
using Domain.Logic;
using Domain.Logic.BusinessLogic;
using Domain.Logic.ExternalServices.FileManager;
using Domain.Logic.Interfaces;
using Domain.Logic.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Middleware;

namespace WebApp
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
            this.Services.AddHttpClient();

            this.Services.AddScoped<RazorViewRender>();
            this.Services.AddScoped<ExceptionHandlerMiddleware>();

            // Operation context
            this.Services.AddScoped<IOperationContext, WebOperationContext>();

            // Services.
            this.Services.AddScoped<IAccountService, AccountService>();
            this.Services.AddScoped<IProductService, ProductService>();
            this.Services.AddScoped<IService<Category>, CategoryService>();
            this.Services.AddScoped<IUserService, UserService>();
            this.Services.AddScoped<IBackOfficeUserService, BackOfficeUserService>();
            this.Services.AddScoped<IFrontUserService, FrontUserService>();
            this.Services.AddScoped<IFileManagerService, FileManagerService>();
            this.Services.AddScoped<IHolidayService, HolidayService>();
            this.Services.AddScoped<ITenantService, TenantService>();
            this.Services.AddScoped<IGameService, GameService>();
            this.Services.AddScoped<IEventService, EventService>();
            this.Services.AddScoped<IPeliculaService, PeliculaService>();
            this.Services.AddScoped<IThemeService, ThemeService>();
            this.Services.AddScoped<IQuestionService, QuestionService>();
            this.Services.AddScoped<IActorService, ActorService>();
            this.Services.AddScoped<IService<Genero>, GeneroService>();


            // BusinessLogic
            this.Services.AddScoped<IHolidayBusinessLogic, HolidayBusinessLogic>();
            this.Services.AddScoped<IStatisticsBusinessLogic, StatisticsBusinessLogic>();

            this.Services.AddApplicationInsightsTelemetry();

            // Configuration
            this.Services.AddScoped<IRealmConfiguration, RealmConfiguration>();
            this.Services.AddScoped<IGoogleMapsConfiguration, GoogleMapsConfiguration>();

            #region EmailService
            //TODO:  
            //Use AppSettings / User Secret

            this.Services.AddTransient<IEmailSender, EmailSender>();

            var smtpConfig = new SmtpConfiguration().Bind(Configuration);

            //TODO: If not configured there must be an exception telling so
            this.Services.AddFluentEmail(smtpConfig.Address)
                .AddRazorRenderer()
                .AddSmtpSender(smtpConfig.GenerateSmtpClient());
            #endregion

            #region Authentication
            this.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Identity/Account/Login";
                config.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

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

            #endregion

        }
    }
}