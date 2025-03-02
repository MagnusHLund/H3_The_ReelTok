using reeltok.api.videos.DTOs;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;

namespace reeltok.api.videos.Services
{
    public class LikesService : BaseService, ILikesService
    {
        private const string UsersMicroServiceBaseUrl = "http://localhost:5001/api/users";
        private readonly IHttpService _httpService;
        private readonly ILikesRepository _likesRepository;
        public LikesService(IHttpService httpService, ILikesRepository likesRepository)
        {
            _httpService = httpService;
            _likesRepository = likesRepository;
        }

        public async Task<bool> LikeVideoAsync(Guid userId, Guid videoId)
        {
            ServiceAddLikeRequestDto requestDto = new ServiceAddLikeRequestDto(userId, videoId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/addLike");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceAddLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId)
        {
            ServiceRemoveLikeRequestDto requestDto = new ServiceRemoveLikeRequestDto(userId, videoId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/removeLike");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceRemoveLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<VideoLikesEntity>> GetLikesForVideos(Guid userId, List<Guid> videoIds)
        {
            bool hasUserLikedVideo = await HasUserLikedVideoAsync(userId, videoIds).ConfigureAwait(false);
            uint videoTotalLikes = await _likesRepository.GetTotalLikesForVideosAsync(videoIds).ConfigureAwait(false);

            return new VideoLikesEntity(videoIds, videoTotalLikes, hasUserLikedVideo);
        }

        private async Task<bool> HasUserLikedVideoAsync(Guid userId, List<Guid> videoIds)
        {
            ServiceUserLikedVideoRequestDto requestDto = new ServiceUserLikedVideoRequestDto(userId, videoIds);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/userLikedVideo");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUserLikedVideoRequestDto, ServiceUserLikedVideoResponseDto>(requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceUserLikedVideoResponseDto responseDto)
            {
                return responseDto.UserHasLikedVideo;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
