namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IRecommendationsService
    {
        Task<bool> UpdateTotalTimesUserWatchedVideosAsync(List<Guid> videoIds);
    }
}
