using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetails
{
    public class UsersServiceGetUserDetailsForVideoResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Users")]
        public List<UserEntity> VideoCreators { get; set; }

        public UsersServiceGetUserDetailsForVideoResponseDto(List<UserEntity> videoCreators, bool success) : base(success)
        {
            VideoCreators = videoCreators;
        }
    }
}
