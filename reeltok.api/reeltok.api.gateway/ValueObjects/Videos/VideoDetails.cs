using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.ValueObjects
{
    public class VideoDetails
    {
        public string Title { get; }
        public string Description { get; }
        public CategoryType Category { get; }

        public VideoDetails(string title, string description, CategoryType category)
        {
            Title = title;
            Description = description;
            Category = category;
        }
    }
}
