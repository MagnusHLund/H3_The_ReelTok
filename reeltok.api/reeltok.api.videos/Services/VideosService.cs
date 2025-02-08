using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Services
{
    public class VideosService : IVideosService
    {
        private readonly IStorageService _storageService;
        private readonly IHttpService _httpService;
        public VideosService(IStorageService storageService, IHttpService httpService) {
            _storageService = storageService;
            _httpService = httpService;
        }

        public Task<bool> DeleteVideo(Guid userId, Guid videosId)
        {
            // Removes the video in the Videos database and file server
            throw new NotImplementedException();
        }

        public Task<List<Video>> GetVideosForFeed(Guid userId, byte amount)
        {
            // Calls recommendations API, receives users preferred category.
            throw new NotImplementedException();
        }

        public Task<List<Video>> GetVideosForProfile(Guid userId, byte amountToReturn, uint amountReceived)
        {
            // Gets videos uploaded by the user that you're currently on
            throw new NotImplementedException();
        }

        public Task<Video> UploadVideo(VideoUpload video)
        {
            if(video.VideoFile.Length == 0) {
                throw new ArgumentException("Invalid video file!");
            }

            // Uploads a video to the file server
            throw new NotImplementedException();
        }
    }
}
