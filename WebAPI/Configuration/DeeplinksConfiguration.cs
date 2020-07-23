using WebAPI.Interfaces.Configuration;

namespace WebAPI.Helpers.Configuration
{
    public class DeeplinksConfiguration : IDeeplinksConfiguration
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
    }
}