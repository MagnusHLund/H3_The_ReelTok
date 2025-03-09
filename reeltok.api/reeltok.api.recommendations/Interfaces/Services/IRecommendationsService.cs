namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IRecommendationsService
    {
        Task<List<string>> GetAllCategoriesAsync();
        Task<List<Guid>> GetTopVideoByUserInterestAsync(Guid userId, int amount);
    }
}
