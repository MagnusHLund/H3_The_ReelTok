using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryResponseDto")]
    public class ServiceChangeRecommendedCategoryResponseDto : BaseResponseDto
    {
        // TODO: Test if its an issue assigning a value to the only parameter, when also having a parameterless constructor.
        public ServiceChangeRecommendedCategoryResponseDto(bool success = true) : base(success) { }
        public ServiceChangeRecommendedCategoryResponseDto() { }
    }
}