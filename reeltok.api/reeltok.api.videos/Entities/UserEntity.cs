using Newtonsoft.Json;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Entities
{
    public class UserEntity
    {
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [JsonProperty("UserDetails")]
        public UserDetails UserDetails { get; set; }

        public UserEntity(Guid userId, UserDetails userDetails)
        {
            UserId = userId;
            UserDetails = userDetails;
        }
    }
}