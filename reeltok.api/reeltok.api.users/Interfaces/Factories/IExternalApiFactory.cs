namespace reeltok.api.users.Interfaces.Factories
{
    public interface IExternalApiFactory
    {
        Uri GetRecommendationsApiUrl(string endpoint = "");
        Uri GetAuthApiUrl(string endpoint = "");
    }
}