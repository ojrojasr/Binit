using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Binit.Framework.Helpers
{
    public class Versioning
    {
        public static void UpdateVersion(IConfiguration configuration)
        {
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            string buildDate = GetBuildDate(callingAssembly);
            string versionDate = GetVersionDate(configuration);

            if (buildDate != versionDate)
                SetConfigDate(configuration, buildDate, callingAssembly);
        }

        private static string GetBuildDate(Assembly assembly)
        {
            var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.BuildTimeStamp.txt");

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static string GetVersionDate(IConfiguration configuration)
        {
            string version = configuration.GetSection("Version").Value;
            string versionDate = version.Split('.').Last();

            return versionDate;
        }

        private static void SetConfigDate(IConfiguration configuration, string buildDate, Assembly assembly)
        {
            var versionAssembly = assembly.GetName().Version;

            configuration.GetSection("Version").Value = $"{versionAssembly.ToString()}.{buildDate}";
        }

        private const string dateTimeFormat = "yyMMddHHmm";
    }
}
