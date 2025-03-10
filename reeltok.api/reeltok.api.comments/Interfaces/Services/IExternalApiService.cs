namespace reeltok.api.comments.Interfaces.Services
{
    public interface IExternalApiService
    {
        Task EnsureVideoIdExistAsync(Guid videoId);
    }
}