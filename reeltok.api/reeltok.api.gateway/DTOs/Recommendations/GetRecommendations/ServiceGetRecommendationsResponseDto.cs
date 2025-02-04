using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsResponseDto")]
    public class ServiceGetRecommendationsResponseDto : BaseResponseDto
    {
        // TODO: Test if its an issue assigning a value to the only parameter, when also having a parameterless constructor.
        public ServiceGetRecommendationsResponseDto(bool success = true) : base(success) { }
        public ServiceGetRecommendationsResponseDto() { }
    }
}