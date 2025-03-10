using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.DTOs.DoesVideoIdExist
{
    public class VideosServiceDoesVideoIdExistRequestDto
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        public VideosServiceDoesVideoIdExistRequestDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}