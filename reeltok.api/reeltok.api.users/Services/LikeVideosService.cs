using reeltok.api.users.Entities;
using reeltok.api.users.factories;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class LikeVideosService : ILikeVideosService
    {
        private readonly ILikeVideosRepository _likeVideoRepository;
        private readonly IUsersService _usersService;

        public LikeVideosService(ILikeVideosRepository likeVideoRepository, IUsersService usersService)
        {
            _likeVideoRepository = likeVideoRepository;
            _usersService = usersService;
        }

        public async Task<bool> AddToLikedVideosAsync(LikedDetails likedDetails)
        {
            // Ensure the user exists
            await _usersService.GetUserByIdAsync(likedDetails.UserId).ConfigureAwait(false);

            LikedVideoEntity likedVideo = LikedVideoFactory.CreateLikedVideoEntity(likedDetails);
            bool IsLikedVideoAdded = await _likeVideoRepository.AddToLikedVideoAsync(likedVideo)
                .ConfigureAwait(false);

            return IsLikedVideoAdded;
        }

        public async Task<bool> RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            bool IsLikedVideoRemoved = await _likeVideoRepository.RemoveFromLikedVideoAsync(userId, likedVideoId)
                .ConfigureAwait(false);

            return IsLikedVideoRemoved;
        }
    }
}