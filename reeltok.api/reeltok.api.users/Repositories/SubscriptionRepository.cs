using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly UserDbContext _context;

        public SubscriptionRepository(UserDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is to add a user into the list of people you are following
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public async Task<bool> AddUserToSubscriptionAsync(Subscription subscription)
        {
            Subscription DbSubscription = (await _context.Subscriptions.AddAsync(subscription).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return DbSubscription != null;
        }

        /// <summary>
        /// This is for the people who are following a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId)
        {
            return await _context.Subscriptions.Where(s => s.SubDetails.SubscriberUserId == userId).Select(s => s.SubDetails.SubscribingToUserId).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// This is for the people who a user is following
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId)
        {
            return await _context.Subscriptions.Where(s => s.SubDetails.SubscribingToUserId == userId).Select(s => s.SubDetails.SubscriberUserId).ToListAsync().ConfigureAwait(false);
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
            Subscription? subscription = _context.Subscriptions.FirstOrDefault(s => s.SubDetails.SubscriberUserId == userId && s.SubDetails.SubscribingToUserId == subscriptionUserId);

            if (subscription == null)
            {
                return false;
            }

            _context.Subscriptions.Remove(subscription);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
