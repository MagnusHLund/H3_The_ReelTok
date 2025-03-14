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
        public async Task<List<Guid>> GetSubscribersByUserIdAsync(Guid userId, int pageNumber, byte pageSize)
        {
            int skip = pageNumber * pageSize;

            List<Guid> subscriberList = await _context.Subscriptions
                .Where(s => s.UserId == userId)
                .Skip(skip)
                .Select(s => s.SubscribingToUserId)
                .ToListAsync()
                .ConfigureAwait(false);

            return subscriberList;
        }

        /// <summary>
        /// This is for the people who a user is following
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetSubscriptionsByUserIdAsync(Guid userId, int pageNumber, byte pageSize)
        {
            int skip = pageNumber * pageSize;

            List<Guid> subscriptionList = await _context.Subscriptions
                .Where(s => s.SubscribingToUserId == userId)
                .Skip(skip)
                .Select(s => s.UserId)
                .ToListAsync()
                .ConfigureAwait(false);

            return subscriptionList;
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

        /// <summary>
        /// Gets the total number of subscribers (followers) for a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetSubscribersCountAsync(Guid userId)
        {
            return await _context.Subscriptions
                .CountAsync(s => s.UserId == userId)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the total number of users that a user is following (subscriptions).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GetSubscriptionsCountAsync(Guid userId)
        {
            return await _context.Subscriptions
                .CountAsync(s => s.SubscribingToUserId == userId)
                .ConfigureAwait(false);
        }

    }
}
