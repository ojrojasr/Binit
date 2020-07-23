using Binit.Framework.ExceptionHandling;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.ExceptionHandling;
using DAL;
using Domain.Entities.Log;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Domain.Logic.Logging;
using Domain.Logic.Logging.Elmah;
using ElmahCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Domain.Logic
{
    /// <summary>
    /// IServiceInjection base implementation.
    /// This implementation registers all the required services from the Data Access Layer.
    /// </summary>
    public class ServiceInjectionBase : IServiceInjection
    {

        /// <summary>
        /// Use the configuration property to access values from the current project's config file.
        /// </summary>   
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Use the services property to register new services (E.g: using the AddScopped method).
        /// </summary> 
        public IServiceCollection Services { get; }

        /// <summary>
        /// Base constructor. Sets the Configuration and Services properties.
        /// </summary>   
        public ServiceInjectionBase(IServiceCollection services, IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.Services = services;
        }

        /// <summary>
        /// Calls RegisterDALServices & RegisterServices in that order.
        /// Call this method from Startup.ConfigureServices to register the required services for later injections.
        /// </summary> 
        public void Initialize()
        {
            this.RegisterDALServices();
            this.RegisterServices();
        }

        /// <summary>
        /// Registers all required services from the Data Access Layer. 
        /// This method should only be implemented on the Domain.Logic Layer.
        /// </summary> 
        public void RegisterDALServices()
        {
            // SQL database for entities.
            this.Services.AddDbContext<ModelDbContext>(options =>
            {
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Scoped);

            // SQLite database for error and audit logs.
            this.Services.AddDbContext<LogDbContext>(options =>
            {
                options.UseSqlite(this.Configuration.GetConnectionString("LogConnection"));
            }, ServiceLifetime.Scoped);

            this.Services.AddScoped<ModelDbContext>();
            this.Services.AddScoped<LogDbContext>();

            // We'll need to Inject UnitOfWork dependencies explicitly
            // because we need to store entities and logs in different Databases
            // meaning that'll need different DbContext implementations.
            this.Services.AddScoped<IUnitOfWork>((provider) =>
            {
                var logDbContext = provider.GetRequiredService<LogDbContext>();
                var modelDbContext = provider.GetRequiredService<ModelDbContext>();
                return new UnitOfWork(modelDbContext, logDbContext);
            });

            this.Services.AddScoped<ILogger, Logger>();

            this.Services.AddScoped<IExceptionManager, ExceptionManager>();

            // Generic service.
            this.Services.AddScoped(typeof(IService<>), typeof(Service<>));

            // Generic view service.
            this.Services.AddScoped(typeof(IViewService<>), typeof(ViewService<>));

            // Generic SP service.
            this.Services.AddScoped(typeof(ISPService<>), typeof(SPService<>));

            // Generic service for tenant dependents.
            this.Services.AddScoped(typeof(IServiceTenantDependent<>), typeof(ServiceTenantDependents<>));

            this.Services.AddScoped<ErrorLogService>();

            this.Services.AddScoped<AuditLogService>();

            this.Services.AddElmah<ErrorLogger>();
        }

        /// <summary>
        /// Registers all required services from the Domain.Logic Layer.
        /// Override this method to register the required services for the Website or WebApp project.
        /// </summary>
        public virtual void RegisterServices() { }
    }
}