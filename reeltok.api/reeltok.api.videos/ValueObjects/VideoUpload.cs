using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.ValueObjects
{
    public class VideoUpload
    {
        [Required]
        [JsonProperty("VideoDetails")]
        public VideoDetails VideoDetails { get; }

        [Required]
        [JsonProperty("VideoFile")]
        public IFormFile VideoFile { get; }

        public VideoUpload(VideoDetails videoDetails, IFormFile videoFile)
        {
            VideoDetails = videoDetails;
            VideoFile = videoFile;
        }
    }
}
