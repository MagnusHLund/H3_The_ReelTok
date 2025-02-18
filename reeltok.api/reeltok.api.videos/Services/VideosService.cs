using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Services
{
    public class VideosService : BaseService, IVideosService
    {
        private readonly IVideosRepository _videosRepository;
        private readonly IStorageService _storageService;
        private readonly IHttpService _httpService;
        public VideosService(IVideosRepository videosRepository, IStorageService storageService, IHttpService httpService)
        {
            _videosRepository = videosRepository;
            _storageService = storageService;
            _httpService = httpService;
        }

        public async Task DeleteVideoAsync(Guid userId, Guid videosId)
        {
            string streamUri = await _videosRepository.DeleteVideoAsync(userId, videosId).ConfigureAwait(false);
            await _storageService.RemoveVideoFromFileServerAsync(streamUri).ConfigureAwait(false);
        }

        public Task<List<VideoEntity>> GetVideosForFeedAsync(Guid userId, byte amount)
        {
            // Calls recommendations API, receives users preferred category.
            throw new NotImplementedException();
        }

        public async Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize)
        {
            // Gets videos uploaded by the user that you're currently on
            List<VideoEntity> videosUploadedByUser = await _videosRepository
                .GetVideosForProfileAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            

            return videosUploadedByUser;
        }

        public async Task<VideoEntity> UploadVideoAsync(VideoUpload video, Guid userId)
        {
            await StorageService.EnsureValidFileUploadAsync(video.VideoFile).ConfigureAwait(false);
            VideoEntity videoEntity = await _videosRepository.CreateVideoAsync().ConfigureAwait(false);

            string streamUrlResource = await _storageService.UploadVideoToFileServerAsync(
                video.VideoFile, videoEntity.VideoId, videoEntity.UserId)
                .ConfigureAwait(false);

            videoEntity = await _videosRepository.UpdateVideoStreamUrl(
                videoEntity.VideoId, streamUrlResource)
                .ConfigureAwait(false);

            return videoEntity;
        }
    }
}
