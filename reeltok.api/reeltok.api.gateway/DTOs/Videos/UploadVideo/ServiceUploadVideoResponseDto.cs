using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class ServiceUploadVideoResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("video")]
        public VideoForFeedUsingUnixTimeEntity Video { get; set; }

        public ServiceUploadVideoResponseDto(VideoForFeedUsingUnixTimeEntity video, bool success = true) : base(success)
        {
            Video = video;
        }
    }
}
