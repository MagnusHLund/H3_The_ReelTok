using Newtonsoft.Json;
using reeltok.api.users.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.Entities
{
    public class ExternalUserEntity
    {
        [Key]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; } = Guid.Empty;

        [JsonProperty("UserDetails")]
        public UserDetails UserDetails { get; set; }

        public ExternalUserEntity(Guid userId, UserDetails details)
        {
            UserId = userId;
            UserDetails = details;
        }

        private protected ExternalUserEntity() { }
    }
}
