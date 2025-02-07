using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoRequestDto")]
    public class GatewayUploadVideoRequestDto
    {
        [XmlElement(elementName: "Title")]
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [XmlElement(elementName: "Description")]
        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        [XmlElement(elementName: "Tag")]
        [Required]
        [StringLength(30)]
        public RecommendedCategories Tag { get; set; }
        [XmlElement(elementName: "Video")]
        [Required]
        public IFormFile Video { get; set; }

        public GatewayUploadVideoRequestDto(string title, string description, RecommendedCategories tag, IFormFile video)
        {
            Title = title;
            Description = description;
            Tag = tag;
            Video = video;
        }
    }
}