using reeltok.api.gateway.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Recommendations.ChangeRecommendations
{
    public class GatewayChangeRecommendedCategoryRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public List<CategoryType> Category { get; set; }

        public GatewayChangeRecommendedCategoryRequestDto(Guid userId, List<CategoryType> category)
        {
            UserId = userId;
            Category = category;
        }
    }
}
