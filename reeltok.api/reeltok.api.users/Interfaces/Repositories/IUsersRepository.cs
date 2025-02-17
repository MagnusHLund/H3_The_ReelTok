using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> UpdateUserAsync(User user, Guid userId);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}