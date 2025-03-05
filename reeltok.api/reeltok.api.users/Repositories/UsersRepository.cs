using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserDbContext _context;

        public UsersRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            UserEntity DbUser = (await _context.Users.AddAsync(user).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return DbUser;
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User with id {userId} not found!");
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            UserEntity userEntity = await _context.Users.FindAsync(userId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User with id {userId} not found!");

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<List<UserEntity>> GetUsersByUserIdsAsync(List<Guid> userIds)
        {
            return await _context.Users.Where(u => userIds.Contains(u.UserId)).ToListAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException("No users found with the provided ids!");
        }
    }
}