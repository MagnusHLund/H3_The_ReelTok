using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface IUsersService
    {
        Task<UserProfileData> LoginUser(string email, string password);
        Task<UserProfileData> CreateUser(string email, string username, string password);
        Task<UserProfileData> GetUserProfileData(Guid userId);
        Task<EditableUserDetails> UpdateUserDetails(string username, string email);
        Task<string> UpdateProfilePicture(IFormFile image);
        Task<List<UserDetails>> GetAllSubscriptionsForUser(Guid userId);
        Task<List<UserDetails>> GetAllSubscribingToUser(Guid userId);
    }
}
