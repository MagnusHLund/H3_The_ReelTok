using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs.GetUserInterest
{
    public class GetUserInterestResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Interest")]
        public CategoryType Interest { get; set; }

        public GetUserInterestResponseDto(CategoryType interest, bool success = true) : base(success)
        {
            Interest = interest;
        }
    }
}