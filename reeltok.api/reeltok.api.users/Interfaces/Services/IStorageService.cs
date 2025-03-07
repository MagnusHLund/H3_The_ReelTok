namespace reeltok.api.users.Interfaces.Services
{
    public interface IStorageService
    {
        Task<string> UploadProfilePictureToFileServerAsync(IFormFile imageFile, Guid UserId);
    }
}