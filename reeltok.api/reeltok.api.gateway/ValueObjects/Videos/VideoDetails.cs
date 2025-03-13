using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.ValueObjects
{
    public class VideoDetails
    {
        [Required]
        [JsonProperty("Title")]
        public string Title { get; }

        [Required]
        [JsonProperty("Description")]
        public string Description { get; }

        [Required]
        [JsonProperty("Category")]
        public CategoryType Category { get; }

        public VideoDetails(string title, string description, CategoryType category)
        {
            Title = title;
            Description = description;
            Category = category;
        }
    }
}
