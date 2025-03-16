using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.Users
{
    public class UserWithInterestEntity : UserEntity
    {
        [Required]
        [JsonProperty("Interest")]
        public byte Interest { get; set; }

        public UserWithInterestEntity(
            Guid userId,
            UserDetails userDetails,
            HiddenUserDetails hiddenUserDetails,
            byte interest
        ) : base(userId, userDetails, hiddenUserDetails)
        {
            Interest = interest;
        }
    }
}