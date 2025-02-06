using System.Xml.Serialization;
using reeltok.api.videos.Enums;

namespace reeltok.api.videos.ValueObjects
{
    [XmlRoot("VideoDetails")]
    internal class VideoDetails
    {
        [XmlElement("Title")]
        internal string Title { get; }
        [XmlElement("Description")]
        internal string Description { get; }
        [XmlElement("RecommendedCategories")]
        internal RecommendedCategories Tag { get; }

        public VideoDetails(string title, string description, RecommendedCategories tag)
        {
            Title = title;
            Description = description;
            Tag = tag;
        }
    }
}