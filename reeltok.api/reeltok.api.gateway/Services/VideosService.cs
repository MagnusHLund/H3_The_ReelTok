using System.Threading.Tasks;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;

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



            throw HandleExceptions(request);
        }
        public Task<bool> RemoveLikeFromVideo(Guid VideoId)
        {
            throw HandleExceptions(request);
        }
        public List<Video> GetVideosForFeed(byte amount)
        {
            throw HandleExceptions(request);
        }
        public Task<string> UploadVideo(VideoUpload video)
        {
            throw HandleExceptions(request);
        }
        public Task<bool> DeleteVideo(Guid videoId)
        {
            throw HandleExceptions(request);
        }
    }
}