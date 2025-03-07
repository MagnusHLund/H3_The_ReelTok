namespace reeltok.api.users.Interfaces.Factories
{
    public interface IEndpointFactory
    {
        Uri GetRecommendationsApiUrl(string route);
        Uri GetAuthApiUrl(string route);
    }
}