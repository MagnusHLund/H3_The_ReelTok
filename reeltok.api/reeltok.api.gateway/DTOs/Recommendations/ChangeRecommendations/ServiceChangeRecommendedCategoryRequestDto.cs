using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryRequestDto")]
    public class ServiceChangeRecommendedCategoryRequestDto
    {
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        public ServiceChangeRecommendedCategoryRequestDto(Guid userId, string category)
        {
            UserId = userId;
            Category = category;
        }
    }
}