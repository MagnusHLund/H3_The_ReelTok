using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Repositories;
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

        public Task<bool> DeleteVideoAsync(Guid userId, Guid videosId)
        {
            // Removes the video in the Videos database and file server
            throw new NotImplementedException();
        }

        public Task<List<VideoEntity>> GetVideosForFeedAsync(Guid userId, byte amount)
        {
            // Calls recommendations API, receives users preferred category.
            throw new NotImplementedException();
        }

        public Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, byte amountToReturn, uint amountReceived)
        {
            // Gets videos uploaded by the user that you're currently on
            throw new NotImplementedException();
        }

        public async Task<VideoEntity> UploadVideoAsync(VideoUpload video, Guid userId)
        {
            await StorageService.EnsureValidFileUploadAsync(video.VideoFile).ConfigureAwait(false);

            // TODO: Save video in database, here
            VideoEntity videoEntity = await _videosRepository.CreateVideoAsync().ConfigureAwait(false);

            await _storageService.UploadVideoToFileServerAsync(video.VideoFile, userId, videoEntity.VideoId).ConfigureAwait(false);

            // Uploads a video to the file server
            throw new NotImplementedException();
        }
    }
}
