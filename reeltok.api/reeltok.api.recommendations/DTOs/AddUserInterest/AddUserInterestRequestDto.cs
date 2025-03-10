using Newtonsoft.Json;
using reeltok.api.recommendations.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs.AddUserInterest
{
    public class AddUserInterestRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Interest")]
        public CategoryType Interest { get; set; }

        public AddUserInterestRequestDto(Guid userId, CategoryType interest)
        {
            UserId = userId;
            Interest = interest;
        }
    }
}