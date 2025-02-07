using System.Xml.Serialization;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoRequestDto")]
    public class GatewayUploadVideoRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public RecommendedCategories Tag { get; set; }
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