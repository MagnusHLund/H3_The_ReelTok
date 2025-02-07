using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class UserProfileData
    {
        [Key]
        public Guid UserId { get; set; } = Guid.Empty;
        public UserDetails UserDetails { get; set; }

        public UserProfileData(Guid userId, UserDetails details)
        {
            UserId = userId;
            UserDetails = details;
        }

        // Parameterless constructor for EF Core
        private UserProfileData() { }
    }
}