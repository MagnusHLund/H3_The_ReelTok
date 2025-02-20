using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.ValueObjects
{
    public class RecommendationDetails
    {
        [Required]
        public Guid UserId { get; private set; }

        public RecommendationDetails(Guid userId)
        {
            UserId = userId;
        }

        private RecommendationDetails()
        {

        }

    }
}
