using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Services
{
    public interface IUsersService
    {
        Task<UserEntity> CreateUserAsync(string username, string email, string password, byte interests);
        Task<UserWithSubscriptionCounts> GetUserByIdAsync(Guid userId);
        Task<UserEntity> UpdateUserAsync(Guid userId, string? username, string? email);
        Task<List<UserEntity>> GetUsersByIdsAsync(List<Guid> userIds);
        Task<UserEntity> GetUserByEmail(string email);
        Task<UserEntity> UpdateUserProfilePictureAsync(IFormFile imageFile, Guid userId);
    }
}
