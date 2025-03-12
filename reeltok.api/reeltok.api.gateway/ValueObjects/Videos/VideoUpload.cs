using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.ValueObjects
{
    public class VideoUpload
    {
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public CategoryType Category { get; set; }
        public IFormFile VideoFile { get; }

        public VideoUpload(string title, string description, CategoryType category, IFormFile videoFile)
        {
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }

        public VideoUpload(Guid userId, string title, string description, CategoryType category, IFormFile videoFile)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }
    }
}
