using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid userId);
    }
}