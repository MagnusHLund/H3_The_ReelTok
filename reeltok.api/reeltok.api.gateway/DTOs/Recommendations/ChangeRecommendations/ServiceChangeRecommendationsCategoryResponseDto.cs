using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendationsCategoryResponseDto")]
    public class ServiceChangeRecommendationsCategoryResponseDto : BaseResponseDto
    {
        public ServiceChangeRecommendationsCategoryResponseDto(bool success) : base(success) { }
        public ServiceChangeRecommendationsCategoryResponseDto() { }
    }
}