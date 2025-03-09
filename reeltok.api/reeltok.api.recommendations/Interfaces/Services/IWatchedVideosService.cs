namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IWatchedVideosService
    {
        Task UpdateTotalTimesUserWatchedVideosAsync(Guid userId, List<Guid> watchedVideoIds);
    }
}
