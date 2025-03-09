using Newtonsoft.Json;
using reeltok.api.recommendations.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs.AddUserInterest
{
    public class AddUserInterestResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Interest")]
        public CategoryType Interest { get; set; }

        public AddUserInterestResponseDto(CategoryType interest, bool success = true) : base(success)
        {
            Interest = interest;
        }
    }
}