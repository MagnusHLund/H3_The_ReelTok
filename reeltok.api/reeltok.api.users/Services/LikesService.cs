using reeltok.api.users.Entities;
using reeltok.api.users.factories;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class LikesService : ILikesService
    {
        private readonly ILikesRepository _likeVideoRepository;
        private readonly IUsersService _usersService;

        public LikesService(ILikesRepository likeVideoRepository, IUsersService usersService)
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

        public async Task<List<HasUserLikedVideoEntity>> GetHasUserLikedVideosAsync(Guid userId, List<Guid> videoIds)
        {
            List<HasUserLikedVideoEntity> likedVideos = await _likeVideoRepository.CheckUserLikesForVideosAsync(userId, videoIds).ConfigureAwait(false);

            Dictionary<Guid, bool> likedVideosDictionary = likedVideos.ToDictionary(lv => lv.VideoId, lv => lv.HasUserLikedVideo);

            List<HasUserLikedVideoEntity> HasUserLikedVideos = videoIds
                .Select(videoId => new HasUserLikedVideoEntity(videoId,likedVideosDictionary
                .ContainsKey(videoId) && likedVideosDictionary[videoId]))
                .ToList();

            return HasUserLikedVideos;
        }
    }
}
