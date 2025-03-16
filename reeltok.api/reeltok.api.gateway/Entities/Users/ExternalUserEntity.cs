using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.Users
{
    public class ExternalUserEntity
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("UserDetails")]
        public UserDetails UserDetails { get; set; }

        public ExternalUserEntity(Guid userId, UserDetails details)
        {
            UserId = userId;
            UserDetails = details;
        }
    }
}
