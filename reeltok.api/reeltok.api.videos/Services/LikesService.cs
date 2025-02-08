using reeltok.api.videos.DTOs;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.DTOs.UserLikedVideo;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Services
{
    public class LikesService : BaseService, ILikesService
    {
        private const string UsersMicroServiceBaseUrl = "http://localhost:5001/users";
        private readonly IHttpService _httpService;
        private readonly ILikesRepository _likesRepository;
        public LikesService(IHttpService httpService, ILikesRepository likesRepository)
        {
            _httpService = httpService;
            _likesRepository = likesRepository;
        }

        public async Task<bool> LikeVideo(Guid userId, Guid videoId)
        {
            ServiceAddLikeRequestDto requestDto = new ServiceAddLikeRequestDto(userId, videoId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/AddLike");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if(response.Success && response is ServiceAddLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<bool> RemoveLikeFromVideo(Guid userId, Guid videoId)
        {
            ServiceRemoveLikeRequestDto requestDto = new ServiceRemoveLikeRequestDto(userId, videoId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/RemoveLike");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if(response.Success && response is ServiceRemoveLikeResponseDto responseDto)
            {
                return responseDto.Success;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<VideoLikes> GetVideoLikes(Guid userId, Guid videoId)
        {
            bool hasUserLikedVideo = await HasUserLikedVideo(userId, videoId).ConfigureAwait(false);
            uint videoTotalLikes = await _likesRepository.GetTotalVideoLikesAsync(videoId).ConfigureAwait(false);

            return new VideoLikes(videoTotalLikes, hasUserLikedVideo);
        }

        private async Task<bool> HasUserLikedVideo(Guid userId, Guid videoId) {
            ServiceUserLikedVideoRequestDto requestDto = new ServiceUserLikedVideoRequestDto(userId, videoId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/userLikedVideo");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUserLikedVideoRequestDto, ServiceUserLikedVideoResponseDto>(requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if(response.Success && response is ServiceUserLikedVideoResponseDto responseDto)
            {
                return responseDto.UserHasLikedVideo;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
