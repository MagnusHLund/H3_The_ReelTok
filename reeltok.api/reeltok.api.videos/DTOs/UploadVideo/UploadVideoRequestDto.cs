using System.Xml.Serialization;
using reeltok.api.videos.Enums;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("UploadVideoRequestDto")]
    public class UploadVideoRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }

        [XmlElement("Title")]
        public string Title { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Tag")]
        public int Tag { get; set; }

        [XmlElement("VideoFile")]
        public IFormFile VideoFile { get; set; }

        public UploadVideoRequestDto(Guid userId, string title, string description, int tag, IFormFile videoFile)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Tag = tag;
            VideoFile = videoFile;
        }

        public UploadVideoRequestDto() { }
    }
}
