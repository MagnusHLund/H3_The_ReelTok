using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Entities.Users;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IUsersService
    {
        Task<UserEntity> LoginUserAsync(string email, string password);
        Task<UserProfileData> CreateUserAsync(string email, string username, string password);
        Task<UserProfileData> GetUserProfileDataAsync(Guid userId);
        Task<EditableUserDetails> UpdateUserDetailsAsync(string username, string email);
        Task<string> UpdateProfilePictureAsync(IFormFile image);
        Task<List<UserDetails>> GetAllSubscriptionsForUserAsync(Guid userId);
        Task<List<UserDetails>> GetAllSubscribingToUserAsync(Guid userId);
    }
}
