using reeltok.api.gateway.Interfaces.Factories;
using reeltok.api.gateway.Utils;

namespace reeltok.api.gateway.Factories
{
    public class EndpointFactory : IEndpointFactory
    {
        private readonly AppSettingsUtils _appSettingsUtils;

        public EndpointFactory(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public Uri GetUsersApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("UsersApi");
            return EndpointUriBuilder(baseUrl, route);
        }

        public Uri GetVideosApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("VideosApi");
            return EndpointUriBuilder(baseUrl, route);
        }

        public Uri GetAuthApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("AuthApi");
            return EndpointUriBuilder(baseUrl, route);
        }

        public Uri GetRecommendationsApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("RecommendationsApi");
            return EndpointUriBuilder(baseUrl, route);
        }

        public Uri GetCommentsApiUrl(string route)
        {
            string baseUrl = GetConfigurationValue("CommentsApi");
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