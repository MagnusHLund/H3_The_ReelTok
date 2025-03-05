using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<UserEntity> CreateUserAsync(UserEntity user);
        Task<UserEntity> GetUserByIdAsync(Guid userId);
        Task<UserEntity> UpdateUserAsync(UserEntity user);
        Task<bool> DeleteUserAsync(Guid userId);
        Task<List<UserEntity>> GetUsersByUserIdsAsync(List<Guid> userIds);
    }
}