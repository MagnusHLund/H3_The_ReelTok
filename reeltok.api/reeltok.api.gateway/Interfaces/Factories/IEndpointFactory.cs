namespace reeltok.api.gateway.Interfaces.Factories
{
    public interface IEndpointFactory
    {
        Uri GetUsersApiUrl(string route);
        Uri GetVideosApiUrl(string route);
        Uri GetAuthApiUrl(string route);
        Uri GetRecommendationsApiUrl(string route);
        Uri GetCommentsApiUrl(string route);
    }
}