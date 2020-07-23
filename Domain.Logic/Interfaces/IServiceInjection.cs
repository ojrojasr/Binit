using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Logic.Interfaces
{
    /// <summary>
    /// Service Injection Interface.
    /// Defines the required methods and properties to register services from the DAL and UI layers.
    /// </summary>
    public interface IServiceInjection
    {
        /// <summary>
        /// Use the configuration property to access values from the current project's config file.
        /// </summary>   
        IConfiguration Configuration { get; }

        /// <summary>
        /// Use the services property to register new services (E.g: using the AddScopped method).
        /// </summary> 
        IServiceCollection Services { get; }

        /// <summary>
        /// Calls RegisterDALServices & RegisterServices in that order.
        /// Call this method from Startup.ConfigureServices to register the required services for later injections.
        /// </summary> 
        void Initialize();

        /// <summary>
        /// Registers all required services from the Data Access Layer. 
        /// This method should only be implemented on the Domain.Logic Layer.
        /// </summary> 
        void RegisterDALServices();

        /// <summary>
        /// Registers all required services from the Domain.Logic Layer.
        /// </summary> 
        void RegisterServices();
    }
}