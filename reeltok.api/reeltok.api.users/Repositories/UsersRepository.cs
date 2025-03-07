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
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User with id {userId} not found in the database!");
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity updatedUser)
        {
            try
            {
                UserEntity existingUser = await _context.Users
                    .Include(u => u.UserDetails)
                    .Include(u => u.HiddenUserDetails)
                    .FirstOrDefaultAsync(u => u.UserId == updatedUser.UserId).ConfigureAwait(false)
                    ?? throw new KeyNotFoundException($"User with id {updatedUser} not found in the database!");

                _context.Entry(existingUser).CurrentValues.SetValues(updatedUser);

                _context.Entry(existingUser.UserDetails).CurrentValues.SetValues(updatedUser.UserDetails);
                _context.Entry(existingUser.HiddenUserDetails).CurrentValues.SetValues(updatedUser.HiddenUserDetails);

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return existingUser;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Error updating user with id {updatedUser.UserId} in the database!", ex);
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            UserEntity userEntity = await _context.Users.FindAsync(userId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User with id {userId} not found in the database!");

            try
            {
                _context.Users.Remove(userEntity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Failed to delete user with id {userId}!", ex);
            }
        }

        public async Task<List<UserEntity>> GetUsersByUserIdsAsync(List<Guid> userIds)
        {
            return await _context.Users.Where(u => userIds.Contains(u.UserId)).ToListAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException("No users found with the provided user ids!");
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.HiddenUserDetails.Email == email).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"User with email {email} not found in the database!");
        }
    }
}