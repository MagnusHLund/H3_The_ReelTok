using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        private readonly UserDbContext _context;

        public SubscriptionsRepository(UserDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is to add a user into the list of people you are following
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public async Task<bool> AddUserToSubscriptionAsync(SubscriptionEntity subscription)
        {
            SubscriptionEntity subscriptionEntity = (await _context.Subscriptions.AddAsync(subscription)
                .ConfigureAwait(false))
                .Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return subscriptionEntity != null;
        }

        /// <summary>
        /// This is for the people who are following a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetSubscribersByUserIdAsync(Guid userId)
        {
            return await _context.Subscriptions.Where(s => s.UserId == userId).Select(s => s.SubscribingToUserId)
                .ToListAsync().ConfigureAwait(false)
                ?? new List<Guid>();
        }

        /// <summary>
        /// This is for the people who a user is following
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetSubscriptionsByUserIdAsync(Guid userId)
        {
            return await _context.Subscriptions.Where(s => s.SubscribingToUserId == userId)
                .Select(s => s.UserId).ToListAsync().ConfigureAwait(false)
                ?? new List<Guid>();
        }

        /// <summary>
        /// This is to remove a user from the list of people you are following
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="subscriptionUserId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId)
        {
            SubscriptionEntity subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.SubscribingToUserId == subscriptionUserId)
                .ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find subscription with user id {userId} and subscription user id {subscriptionUserId}!");

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
