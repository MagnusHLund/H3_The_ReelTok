using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class ServiceGetUserDetailsForVideoResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("VideoCreator")]
        public List<VideoCreatorEntity> VideoCreators { get; set; }

        public ServiceGetUserDetailsForVideoResponseDto(List<VideoCreatorEntity> videoCreators, bool success) : base(success)
        {
            VideoCreators = videoCreators;
        }
    }
}
