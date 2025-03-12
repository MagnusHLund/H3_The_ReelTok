using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.GetUserInterest
{
    public class RecommendationServiceGetUserInterestResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("UserInterest")]
        public byte UserInterest { get; set; }

        public RecommendationServiceGetUserInterestResponseDto(byte userInterest, bool success = true) : base(success)
        {
            UserInterest = userInterest;
        }
    }
}
