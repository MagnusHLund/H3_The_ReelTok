using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendationsCategoryRequestRecommendationsService")]
    public class ChangeRecommendationsCategoryRequestRecommendationsService
    {
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        public ChangeRecommendationsCategoryRequestRecommendationsService(Guid userId, string category)
        {
            UserId = userId;
            Category = category;
        }
        public ChangeRecommendationsCategoryRequestRecommendationsService() { }
    }
}