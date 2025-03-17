using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Entities.Users;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IUsersService
    {
        Task<UserWithInterestEntity> LoginUserAsync(string email, string password);
        Task<UserWithInterestEntity> CreateUserAsync(string email, string username, string password, CategoryType userInterest);
        Task<ExternalUserEntity> GetUserByIdAsync(Guid userId);
        Task<UserEntity> UpdateUserDetailsAsync(string? username, string? email, CategoryType? interest);
        Task<UserEntity> UpdateProfilePictureAsync(IFormFile image);
        Task<List<ExternalUserEntity>> GetUserSubscriptionsAsync(Guid userId, int pageNumber, byte pageSize);
        Task<List<ExternalUserEntity>> GetUserSubscribersAsync(Guid userId, int pageNumber, byte pageSize);
        Task<bool> SubscribeToUserAsync(Guid subscribeToUserId);
        Task<bool> UnsubscribeToUserAsync(Guid subscribeToUserId);
    }
}
