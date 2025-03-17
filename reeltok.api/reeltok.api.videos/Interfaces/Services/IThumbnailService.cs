namespace reeltok.api.videos.Interfaces.Services
{
    public interface IThumbnailService
    {
        Task<IFormFile> GenerateVideoThumbnailAsync(IFormFile video);
    }
}