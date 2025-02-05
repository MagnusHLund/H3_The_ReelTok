using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.ValueObjects
{
    public class VideoDetails
    {
        public string Title { get; }
        public string Description { get; }
        public RecommendedCategories Tag { get; }

        public VideoDetails(string title, string description, RecommendedCategories tag)
        {
            Title = title;
            Description = description;
            Tag = tag;
        }
    }
}