using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.Users
{
    public class UserEntity : ExternalUserEntity
    {
        [Required]
        [JsonProperty("HiddenUserDetails")]
        public HiddenUserDetails HiddenUserDetails { get; set; }

        public UserEntity(Guid userId, UserDetails userDetails, HiddenUserDetails hiddenUserDetails) : base(userId, userDetails)
        {
            UserId = userId;
            UserDetails = userDetails;
            HiddenUserDetails = hiddenUserDetails;
        }
    }
}
