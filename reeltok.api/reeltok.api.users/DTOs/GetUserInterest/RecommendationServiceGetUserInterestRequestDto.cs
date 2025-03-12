using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.GetUserInterest
{
    public class RecommendationServiceGetUserInterestRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        public RecommendationServiceGetUserInterestRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}
