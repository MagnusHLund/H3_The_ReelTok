using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.UploadVideo
{
    public class UploadVideoResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Video")]
        BaseVideoEntity Video { get; set; }

        public UploadVideoResponseDto(BaseVideoEntity video, bool success = true) : base(success)
        {
            Video = video;
        }
    }
}
