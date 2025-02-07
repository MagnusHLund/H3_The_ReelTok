using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsResponseDto")]
    public class GatewayGetRecommendationsResponseDto : BaseResponseDto
    {
        public GatewayGetRecommendationsResponseDto(bool success = true) : base(success) { }
    }
}