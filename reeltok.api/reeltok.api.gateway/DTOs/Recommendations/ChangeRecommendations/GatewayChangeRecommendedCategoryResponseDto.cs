using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryResponseDto")]
    public class GatewayChangeRecommendedCategoryResponseDto : BaseResponseDto
    {
        public GatewayChangeRecommendedCategoryResponseDto(bool success) : base(success) { }
        public GatewayChangeRecommendedCategoryResponseDto() { }
    }
}