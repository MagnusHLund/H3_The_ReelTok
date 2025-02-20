using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class RecommendationEntity
    {
        [Key]
        public uint RecommendationId { get; private set; }

        [Required]
        public RecommendationDetails RecommendationDetails { get; private set; }

        public List<CategoryEntity> Categories { get; private set; }

        public RecommendationEntity(RecommendationDetails recommendationDetails)
        {
            RecommendationDetails = recommendationDetails;
        }

        private RecommendationEntity() { }
    }
}

