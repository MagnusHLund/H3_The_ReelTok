using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.Users
{
    public class ExternalUserEntity
    {
        public Guid UserId { get; set; }
        public UserDetails UserDetails { get; set; }


        public ExternalUserEntity(Guid userId, UserDetails details)
        {
            UserId = userId;
            UserDetails = details;
        }
    }
}
