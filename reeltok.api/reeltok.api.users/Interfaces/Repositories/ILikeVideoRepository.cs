namespace reeltok.api.users.Interfaces.Repositories
{
    public interface ILikeVideoRepository
    {
        Task<bool> AddToLikedVideoAsync(Guid userId, Guid videoId);
        Task<bool> RemoveFromLikedVideoAsync(Guid userId, Guid videoId);
    }
}