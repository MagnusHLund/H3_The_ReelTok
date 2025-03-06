namespace reeltok.api.users.Interfaces.Services
{
    public interface IExternalApiService
    {
        Task CreateUserInAuthApiAsync(Guid userId, string password);
        Task CreateUserInRecommendationsApiAsync(Guid userId, byte userInterests);
    }
}