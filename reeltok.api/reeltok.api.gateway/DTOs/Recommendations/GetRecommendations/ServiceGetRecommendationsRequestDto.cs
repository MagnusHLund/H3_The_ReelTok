using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsRequestDto")]
    public class ServiceGetRecommendationsRequestDto
    {
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        public ServiceGetRecommendationsRequestDto(Guid userId, string category)
        {
            UserId = userId;
            Category = category;
        }
        public ServiceGetRecommendationsRequestDto() { }
    }
}