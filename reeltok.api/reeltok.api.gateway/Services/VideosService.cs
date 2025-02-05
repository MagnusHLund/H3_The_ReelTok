using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class VideosService : BaseService, IVideosService
    {
        private const string AuthMicroServiceBaseUrl = "http://localhost:5002/videos";
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;
        public VideosService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }
        public async Task<bool> LikeVideo(Guid VideoId)
        {
            Guid userId = await _authService.GetUserIdByToken();
            throw new NotImplementedException();
            //  throw HandleExceptions(request);
        }
        public async Task<bool> RemoveLikeFromVideo(Guid VideoId)
        {
            Guid userId = await _authService.GetUserIdByToken();
            throw new NotImplementedException();
            //throw HandleExceptions(request);
        }
        public async Task<List<Video>> GetVideosForFeed(byte amount)
        {
            Guid userId = await _authService.GetUserIdByToken();
            throw new NotImplementedException();
            //throw HandleExceptions(request);
        }
        public async Task<Video> UploadVideo(VideoUpload video)
        {
            Guid userId = await _authService.GetUserIdByToken();
            throw new NotImplementedException();
            //throw HandleExceptions(request);
        }
        public async Task<bool> DeleteVideo(Guid videoId)
        {
            Guid userId = await _authService.GetUserIdByToken();
            throw new NotImplementedException();
            //throw HandleExceptions(request);
        }
    }
}