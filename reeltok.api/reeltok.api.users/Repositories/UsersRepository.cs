using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserDBContext _context;

        public UsersRepository(UserDBContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            User DbUser = (await _context.Users.AddAsync(user).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return DbUser;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<User?> UpdateUserAsync(User user, Guid userId)
        {
            User? existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId).ConfigureAwait(false);

            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            User updateUser = _context.Users.Update(user).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return updateUser;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            User? userModel = await _context.Users.FindAsync(userId).ConfigureAwait(false);

            if (userModel == null)
            {
                return false;
            }

            _context.Users.Remove(userModel);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}