using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using WebAPI.Helpers.Configuration;

namespace WebAPI.Helpers
{
    public static class DeeplinksHelper
    {
        public static string GetURL(IConfiguration configuration)
        {
            var deeplinksConfiguration = configuration.GetSection<DeeplinksConfiguration>("Deeplinks");
            return $"{deeplinksConfiguration.Scheme}://{deeplinksConfiguration.Host}";
        }

        public static string GetDeeplinkForCompleteInformation(IConfiguration configuration, ExternalLoginInfo info)
        {
            var deeplink = DeeplinksHelper.GetURL(configuration);
            var name = info.Principal.FindFirstValue(ClaimTypes.Name);
            var lastname = info.Principal.FindFirstValue(ClaimTypes.Surname);
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            deeplink += $"/complete-social-auth?name={name}&lastname={lastname}&email={email}&loginProvider={info.LoginProvider}&providerKey={info.ProviderKey}";

            return deeplink;
        }
    }
}