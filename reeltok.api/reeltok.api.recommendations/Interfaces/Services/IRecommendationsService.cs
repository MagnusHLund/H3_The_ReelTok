namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IRecommendationsService
    {
        Task<List<Guid>> GetVideoRecommendationsForUserAsync(Guid userId, byte amountOfVideos);
    }
}
