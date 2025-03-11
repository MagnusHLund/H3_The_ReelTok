using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetails
{
    public class UsersServiceGetUserDetailsForVideoResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("VideoCreator")]
        public List<VideoCreatorEntity> VideoCreators { get; set; }

        public UsersServiceGetUserDetailsForVideoResponseDto(List<VideoCreatorEntity> videoCreators, bool success) : base(success)
        {
            VideoCreators = videoCreators;
        }
    }
}
