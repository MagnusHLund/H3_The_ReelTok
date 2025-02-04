using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendationsCategoryResponseRecommendationsService")]
    public class ChangeRecommendationsCategoryResponseRecommendationsService : BaseResponseDto
    {
        public ChangeRecommendationsCategoryResponseRecommendationsService(bool success) : base(success) { }
        public ChangeRecommendationsCategoryResponseRecommendationsService() { }
    }
}