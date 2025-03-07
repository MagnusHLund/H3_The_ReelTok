using reeltok.api.users.utils;
using reeltok.api.users.Interfaces.Factories;

namespace reeltok.api.users.factories
{
    public class EndpointFactory : IEndpointFactory
    {
        private readonly AppSettingsUtils _appSettingsUtils;

        public EndpointFactory(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public Uri GetRecommendationsApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("RecommendationsApi");
            return EndpointUriBuilder(baseUrl, route);
        }

        public Uri GetAuthApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("AuthApi");
            return EndpointUriBuilder(baseUrl, route);
        }

        private string GetConfigurationValue(string apiName)
        {
            string baseMicroserviceAppSettingsConfigurationKey = "Microservices";
            string configurationKey = $"{baseMicroserviceAppSettingsConfigurationKey}::{apiName}::Url";

            return _appSettingsUtils.GetConfigurationValue(configurationKey);
        }

        private static Uri EndpointUriBuilder(string baseUrl, string route)
        {
            return new Uri($"{baseUrl}/{route}");
        }
    }
}