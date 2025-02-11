namespace reeltok.api.users.Interfaces.Services
{
    public interface ILikeVideoService
    {
        Task<bool> AddToLikedVideosAsync(Guid userId, Guid likedVideoId);
        Task<bool> RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}