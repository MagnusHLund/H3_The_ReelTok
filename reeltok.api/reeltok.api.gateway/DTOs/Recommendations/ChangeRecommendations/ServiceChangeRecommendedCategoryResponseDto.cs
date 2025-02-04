using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryResponseDto")]
    public class ServiceChangeRecommendedCategoryResponseDto : BaseResponseDto
    {
        public ServiceChangeRecommendedCategoryResponseDto(bool success) : base(success) { }
        public ServiceChangeRecommendedCategoryResponseDto() { }
    }
}