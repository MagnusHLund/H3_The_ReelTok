using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("ChangeRecommendedCategoryRequestDto")]
    public class ServiceChangeRecommendedCategoryRequestDto
    {
        [XmlElement(elementName: "UserId")]
        [Required]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "Category")]
        [Required]
        [XmlArray]
        [XmlArrayItem("Category")]

        public List<RecommendedCategories> Category { get; set; }
        public ServiceChangeRecommendedCategoryRequestDto(Guid userId, List<RecommendedCategories> category)
        {
            UserId = userId;
            Category = category;
        }
    }
}
