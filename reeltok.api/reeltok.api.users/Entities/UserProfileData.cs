using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class UserProfileData
    {
        
        public Guid UserId { get; set; } = Guid.Empty;
        public UserDetails Details { get; set; }
        public UserProfileData(Guid userId, UserDetails details)
        {
            UserId = userId;
            Details = details;
        }
    }
}