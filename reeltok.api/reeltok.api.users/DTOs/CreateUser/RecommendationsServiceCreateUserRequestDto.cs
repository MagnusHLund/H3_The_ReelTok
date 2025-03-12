using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.CreateUser
{
    public class RecommendationsServiceCreateUserRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Interests")]
        public byte Interests { get; set; }

        public RecommendationsServiceCreateUserRequestDto(Guid userId, byte interests)
        {
            UserId = userId;
            Interests = interests;
        }
    }
}
