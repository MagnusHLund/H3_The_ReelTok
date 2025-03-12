using Newtonsoft.Json;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class UserWithInterestEntity : ExternalUserEntity
    {
        [JsonProperty("Interest")]
        public byte Interest { get; set; }

        public UserWithInterestEntity(
            Guid userId,
            UserDetails userDetails,
            byte interest
        ) : base(userId, userDetails)
        {
            Interest = interest;
        }
    }
}
