using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface IUsersService
    {
        public Task<UserProfileData> LoginUser(string email, string password);
        public Task<UserProfileData> CreateUser(string email, string username, string password);
        public Task<UserProfileData> GetUserProfileData(Guid userId);
        public Task<UserDetails> UpdateUserDetails(UserProfileData profileData);
        public Task<IFormFile> UpdateProfilePicture(IFormFile image);
        public Task<List<UserDetails>> GetAllUserSubscriptionsForUser(Guid userId);
        public Task<List<UserDetails>> GetAllSubscribingToUser(Guid userId);
    }
}