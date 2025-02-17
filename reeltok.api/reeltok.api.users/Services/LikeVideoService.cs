using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Services
{
    public class LikeVideoService : ILikeVideoService
    {
        private readonly ILikeVideoRepository _likeVideoRepository;

        public LikeVideoService(ILikeVideoRepository likeVideoRepository)
        {
            _likeVideoRepository = likeVideoRepository;
        }

        public async Task<bool> AddToLikedVideosAsync(LikedVideo likedVideo)
        {
            bool IsLikedVideoAdded;

            try
            {
                IsLikedVideoAdded = await _likeVideoRepository.AddToLikedVideoAsync(likedVideo).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Handle the error, you can log it or throw a custom exception if needed
                throw new InvalidOperationException("Liked video addition failed.", ex);
            }
            
            return IsLikedVideoAdded;
        }

        public async Task<bool> RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            bool IsLikedVideoRemoved;

            try
            {
                IsLikedVideoRemoved = await _likeVideoRepository.RemoveFromLikedVideoAsync(userId, likedVideoId).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Handle the error, you can log it or throw a custom exception if needed
                throw new InvalidOperationException("Liked video removal failed.", ex);
            }

            return IsLikedVideoRemoved;
        }
    }
}