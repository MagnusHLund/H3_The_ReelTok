using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendationsCategoryRequestDto")]
    public class ServiceChangeRecommendationsCategoryRequestDto
    {
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        public ServiceChangeRecommendationsCategoryRequestDto(Guid userId, string category)
        {
            UserId = userId;
            Category = category;
        }
        public ServiceChangeRecommendationsCategoryRequestDto() { }
    }
}