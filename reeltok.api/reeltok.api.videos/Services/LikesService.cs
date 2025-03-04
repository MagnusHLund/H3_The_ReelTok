using reeltok.api.videos.DTOs;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Factories;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.DTOs.UserLikedVideo;

namespace reeltok.api.videos.Services
{
    public class LikesService : BaseService, ILikesService
    {
        private const string UsersMicroServiceBaseUrl = "https://localhost:5001/api/likedVideos";
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
            List<HasUserLikedVideoEntity> hasUserLikedVideo = await HasUserLikedVideosAsync(
                userId, videoIds).ConfigureAwait(false);

            List<TotalVideoLikesEntity> videoTotalLikes = await _likesRepository.GetTotalLikesForVideosAsync(
                videoIds).ConfigureAwait(false);

            List<VideoLikesEntity> videoLikes = VideoFactory.CreateVideoLikesEntityList(
                videoIds, hasUserLikedVideo, videoTotalLikes);

            return videoLikes;
        }

        private async Task<List<HasUserLikedVideoEntity>> HasUserLikedVideosAsync(Guid userId, List<Guid> videoIds)
        {
            ServiceUserLikedVideosRequestDto requestDto = new ServiceUserLikedVideosRequestDto(userId, videoIds);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/userLikedVideo");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUserLikedVideosRequestDto, ServiceUserLikedVideosResponseDto>(requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if (response.Success && response is ServiceUserLikedVideosResponseDto responseDto)
            {
                return responseDto.HasUserLikedVideos;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
