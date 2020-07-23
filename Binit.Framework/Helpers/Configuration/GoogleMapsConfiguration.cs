using Binit.Framework.Interfaces.Configuration;
using Microsoft.Extensions.Configuration;

namespace Binit.Framework.Helpers.Configuration
{
    public class GoogleMapsConfiguration : IGoogleMapsConfiguration
    {
        public string ApiKey { get; set; }

        public GoogleMapsConfiguration(IConfiguration configuration)
        {
            this.Bind(configuration);
        }

        public GoogleMapsConfiguration Bind(IConfiguration Configuration)
        {
            Configuration.GetSection("GoogleMapsConfiguration").Bind(this);
            return this;
        }

    }
}