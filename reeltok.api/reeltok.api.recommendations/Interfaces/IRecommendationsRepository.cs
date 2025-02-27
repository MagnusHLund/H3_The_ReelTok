namespace reeltok.api.recommendations.Interfaces
{
    public interface IRecommendationsRepository
    {
        // TODO: Right now most of them are not returning anything, we need to add the return types
        Task AddRecommendationForUserAsync();
        Task AddRecommendationForVideoAsync();
        Task UpdateRecommendationForUserAsync();
    }
}
