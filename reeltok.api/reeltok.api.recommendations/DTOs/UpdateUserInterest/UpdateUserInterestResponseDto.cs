using Newtonsoft.Json;
using reeltok.api.recommendations.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs.UpdateUserInterest
{
    public class UpdateUserInterestResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Interest")]
        public CategoryType Interest { get; set; }

        public UpdateUserInterestResponseDto(CategoryType interest, bool success = true) : base(success)
        {
            Interest = interest;
        }
    }
}