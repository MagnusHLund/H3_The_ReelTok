using reeltok.api.users.utils;
using reeltok.api.users.Interfaces.Factories;

namespace reeltok.api.users.factories
{
    public class ExternalApiFactory : IExternalApiFactory
    {
        private readonly AppSettingsUtils _appSettingsUtils;

        public ExternalApiFactory(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public Uri GetRecommendationsApiUrl(string endpoint = "")
        {
            string baseUrl = GetConfigurationValue("RecommendationsApi");
            return new Uri($"{baseUrl}/{endpoint}");
        }

        public Uri GetAuthApiUrl(string endpoint = "")
        {
            string baseUrl = GetConfigurationValue("AuthApi");
            return new Uri($"{baseUrl}/{endpoint}");
        }

        private string GetConfigurationValue(string apiName)
        {
            string baseMicroserviceAppSettingsConfigurationKey = "Microservices";
            string configurationKey = $"{baseMicroserviceAppSettingsConfigurationKey}::{apiName}::Url";

            return _appSettingsUtils.GetConfigurationValue(configurationKey);
        }
    }
}