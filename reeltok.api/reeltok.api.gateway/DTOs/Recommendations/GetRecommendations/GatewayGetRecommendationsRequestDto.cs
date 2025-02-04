using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsRequestDto")]
    public class GatewayGetRecommendationsRequestDto
    {
        [XmlElement(elementName: "Category")]
        public string Category { get; set; }
        public GatewayGetRecommendationsRequestDto(string category)
        {
            Category = category;
        }
        public GatewayGetRecommendationsRequestDto() { }
    }
}