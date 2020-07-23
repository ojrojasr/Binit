namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtension
    {
        public static T GetSection<T>(this IConfiguration config, string section) where T : new()
        {
            T instance = new T();
            config.GetSection(section).Bind(instance);
            return instance;
        }

        public static T GetAuthenticationSection<T>(this IConfiguration config, string section) where T : new()
        {
            return config.GetSection<T>($"Authentication:{section}");
        }
    }
}
