namespace reeltok.api.videos.Utils
{
    public class AppSettingsUtils
    {
        private readonly IConfiguration _configuration;

        // This is a singleton class
        public AppSettingsUtils(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
