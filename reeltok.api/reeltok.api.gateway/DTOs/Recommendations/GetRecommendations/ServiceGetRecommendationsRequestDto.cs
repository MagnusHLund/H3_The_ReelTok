using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Recommendations.GetRecommendations
{
    public class ServiceGetRecommendationsRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Category { get; set; }

        public ServiceGetRecommendationsRequestDto(Guid userId, string category)
        {
            UserId = userId;
            Category = category;
        }
    }
}
