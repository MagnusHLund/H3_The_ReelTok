using reeltok.api.users.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.Entities
{
    public class UserEntity : ExternalUserEntity
    {
        [Required]
        public HiddenUserDetails HiddenUserDetails { get; set; }

        public UserEntity(Guid userId, UserDetails userDetails, HiddenUserDetails hiddenUserDetails) : base(userId, userDetails)
        {
            UserId = userId;
            UserDetails = userDetails;
            HiddenUserDetails = hiddenUserDetails;
        }

        // Parameterless constructor for EF Core
        private UserEntity() { }
    }
}