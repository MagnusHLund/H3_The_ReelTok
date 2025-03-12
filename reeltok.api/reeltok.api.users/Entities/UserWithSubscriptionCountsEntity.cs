namespace reeltok.api.users.Entities
{
    public class UserWithSubscriptionCounts : ExternalUserEntity
    {
        public int TotalSubscribers { get; set; }
        public int TotalSubscriptions { get; set; }

        public UserWithSubscriptionCounts(ExternalUserEntity user, int totalSubscribers, int totalSubscriptions)
            : base(user.UserId, user.UserDetails)
        {
            TotalSubscribers = totalSubscribers;
            TotalSubscriptions = totalSubscriptions;
        }
    }
}
