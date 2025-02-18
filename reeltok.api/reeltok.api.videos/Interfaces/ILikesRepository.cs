namespace reeltok.api.videos.Interfaces
{
    public interface ILikesRepository
    {
        Task<List<uint>> GetTotalLikesForVideosAsync(Guid videoId);
    }
}
