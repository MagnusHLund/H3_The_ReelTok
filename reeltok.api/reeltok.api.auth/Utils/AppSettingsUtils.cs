namespace reeltok.api.auth.Utils
{
    public class AppSettingsUtils
    {
        private static IConfiguration _configuration;

        // This is a singleton class
        public AppSettingsUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal string GetConfigurationValue(string key)
        {
            string? value = _configuration[key];

            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"Configuration key '{key}' is not provided in appsettings.json.");
            }

            return value;
        }
    }
}
