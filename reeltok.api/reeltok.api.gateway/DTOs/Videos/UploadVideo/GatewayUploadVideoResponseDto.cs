using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class GatewayUploadVideoResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("video")]
        BaseVideoUsingDateTimeEntity Video { get; set; }
        public GatewayUploadVideoResponseDto(BaseVideoUsingDateTimeEntity video, bool success = true) : base(success)
        {
            Video = video;
        }
    }
}
