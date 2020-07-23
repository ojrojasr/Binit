using Microsoft.Extensions.Configuration;

namespace Binit.Framework.Helpers.Configuration
{
    public class LocaleConfiguration
    {
        public string DateTimeFormat { get; set; }

        public LocaleConfiguration(IConfiguration configuration)
        {
            this.Bind(configuration);
        }

        public LocaleConfiguration Bind(IConfiguration Configuration)
        {
            Configuration.GetSection("Locale").Bind(this);
            return this;
        }
    }
}