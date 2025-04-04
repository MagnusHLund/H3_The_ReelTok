using Newtonsoft.Json;
using reeltok.api.users.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.Entities
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
