using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Services
{
    public interface IUsersService
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<User?> UpdateUserAsync(User user, Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);

    }
}