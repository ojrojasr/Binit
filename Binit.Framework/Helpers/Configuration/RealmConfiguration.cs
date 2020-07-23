using Binit.Framework.Interfaces.Configuration;
using Microsoft.Extensions.Configuration;

namespace Binit.Framework.Helpers.Configuration
{
    public class RealmConfiguration : IRealmConfiguration
    {
        public string Name { get; set; }
        public bool AllowSelfSignUp { get; set; }
        public bool Allow2FA { get; set; }

        public RealmConfiguration(IConfiguration configuration)
        {
            this.Bind(configuration);
        }

        public RealmConfiguration Bind(IConfiguration Configuration)
        {
            Configuration.GetSection("Realm").Bind(this);
            return this;
        }
    }
}