using System.Xml.Serialization;
using reeltok.api.videos.Enums;

namespace reeltok.api.videos.ValueObjects
{
    [XmlRoot("VideoDetails")]
    public class VideoDetails
    {
        [XmlElement("Title")]
        public string Title { get; }
        [XmlElement("Description")]
        public string Description { get; }
        [XmlElement("RecommendedCategories")]
        public RecommendedCategories Tag { get; }

        public VideoDetails(string title, string description, RecommendedCategories tag)
        {
            Title = title;
            Description = description;
            Tag = tag;
        }
    }
}
