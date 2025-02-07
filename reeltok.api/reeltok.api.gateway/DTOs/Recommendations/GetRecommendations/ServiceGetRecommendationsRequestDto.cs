using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsRequestDto")]
    public class ServiceGetRecommendationsRequestDto
    {
        [XmlElement(elementName: "UserId")]
        [Required]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "Category")]
        [Required]
        [XmlArray]
        [XmlArrayItem("Category")]
        public string Category { get; set; }
        public ServiceGetRecommendationsRequestDto(Guid userId, string category)
        {
            UserId = userId;
            Category = category;
        }
        public ServiceGetRecommendationsRequestDto() { }
    }
}