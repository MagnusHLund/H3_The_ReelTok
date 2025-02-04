using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryRequestDto")]
    public class GatewayChangeRecommendedCategoryRequestDto
    {
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        public GatewayChangeRecommendedCategoryRequestDto(string category)
        {
            Category = category;
        }
        public GatewayChangeRecommendedCategoryRequestDto() { }
    }
}