using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryRequestDto")]
    public class GatewayChangeRecommendedCategoryRequestDto
    {
        [XmlElement(elementName: "UserId")]
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [XmlElement(elementName: "Category")]
        [XmlArray]
        [XmlArrayItem("Category")]
        public List<RecommendedCategories> Category { get; set; }
        public GatewayChangeRecommendedCategoryRequestDto(Guid userId, List<RecommendedCategories> category)
        {
            UserId = userId;
            Category = category;
        }
    }
}
