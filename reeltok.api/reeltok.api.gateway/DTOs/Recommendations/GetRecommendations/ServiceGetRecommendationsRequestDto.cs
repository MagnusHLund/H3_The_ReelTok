using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsRequestDto")]
    public class ServiceGetRecommendationsRequestDto
    {
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        public ServiceGetRecommendationsRequestDto(Guid userId, string category)
        {
            UserId = userId;
            Category = category;
        }
        public ServiceGetRecommendationsRequestDto() { }
    }
}