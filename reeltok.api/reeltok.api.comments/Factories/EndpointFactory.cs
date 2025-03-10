using reeltok.api.comments.Utils;
using reeltok.api.comments.Interfaces.Factories;

namespace reeltok.api.comments.Factories
{
    public class EndpointFactory : IEndpointFactory
    {
        private readonly AppSettingsUtils _appSettingsUtils;

        public EndpointFactory(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public Uri GetVideosApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("VideosApi");
            return EndpointUriBuilder(baseUrl, route);
        }

        private string GetConfigurationValue(string apiName)
        {
            string baseMicroserviceAppSettingsConfigurationKey = "Microservices";
            string configurationKey = $"{baseMicroserviceAppSettingsConfigurationKey}:{apiName}:Url";

            return _appSettingsUtils.GetConfigurationValue(configurationKey);
        }

        private static Uri EndpointUriBuilder(string baseUrl, string route)
        {
            return new Uri($"{baseUrl}/{route}");
        }
    }
}