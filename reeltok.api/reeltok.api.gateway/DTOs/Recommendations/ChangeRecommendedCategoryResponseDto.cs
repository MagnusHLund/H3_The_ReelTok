using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("LoadCommentsRequestDto")]
    public class ChangeRecommendedCategoryResponseDto : BaseResponseDto
    {
        public ChangeRecommendedCategoryResponseDto(bool success) : base(success) { }
        public ChangeRecommendedCategoryResponseDto() { }
    }
}