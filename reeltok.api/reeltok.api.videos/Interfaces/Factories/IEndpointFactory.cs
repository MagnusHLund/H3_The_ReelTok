namespace reeltok.api.videos.Interfaces.Factories
{
    public interface IEndpointFactory
    {
        Uri GetRecommendationsApiUrl(string route);
        Uri GetUsersApiEndpoint(string route);
    }
}