using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryRequestDto")]
    public class ChangeRecommendedCategoryRequestDto
    {
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        public ChangeRecommendedCategoryRequestDto(string category)
        {
            Category = category;
        }
        public ChangeRecommendedCategoryRequestDto() { }
    }
}