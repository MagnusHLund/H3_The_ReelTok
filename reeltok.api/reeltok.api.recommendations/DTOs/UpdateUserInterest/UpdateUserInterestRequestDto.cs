using Newtonsoft.Json;
using reeltok.api.recommendations.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    public class UpdateUserInterestRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Interest")]
        public CategoryType Interest { get; set; }

        public UpdateUserInterestRequestDto(Guid userId, CategoryType interest)
        {
            UserId = userId;
            Interest = interest;
        }
    }
}
