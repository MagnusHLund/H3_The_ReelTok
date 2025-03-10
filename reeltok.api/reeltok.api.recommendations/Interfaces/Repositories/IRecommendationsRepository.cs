namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IRecommendationsRepository
    {
        Task<List<Guid>> GetRecommendedVideosByUserAsync(Guid userId, byte amountOfVideos);
    }
}