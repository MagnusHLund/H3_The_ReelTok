using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.ValueObjects
{
    public class VideoDetails
    {
        [Required]
        [JsonProperty("Title")]
        public string Title { get; }

        [Required]
        [JsonProperty("Description")]
        public string Description { get; }

        public VideoDetails(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}
