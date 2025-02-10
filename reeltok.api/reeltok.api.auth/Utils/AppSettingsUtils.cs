namespace reeltok.api.auth.Utils
{
    internal class AppSettingsUtils
    {
        private static IConfiguration _configuration;

        // This is a singleton class
        internal AppSettingsUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Ensures thread safety
        static AppSettingsUtils()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        internal static string GetConfigurationValue(string key)
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
