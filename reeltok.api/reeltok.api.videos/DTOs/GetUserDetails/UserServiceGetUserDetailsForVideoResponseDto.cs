using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class UserServiceGetUserDetailsForVideoResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("VideoCreator")]
        public List<VideoCreatorEntity> VideoCreators { get; set; }

        public UserServiceGetUserDetailsForVideoResponseDto(List<VideoCreatorEntity> videoCreators, bool success) : base(success)
        {
            VideoCreators = videoCreators;
        }
    }
}
