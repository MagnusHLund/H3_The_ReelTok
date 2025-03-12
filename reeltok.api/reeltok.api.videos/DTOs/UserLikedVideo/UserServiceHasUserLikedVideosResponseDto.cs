using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    public class UserServiceHasUserLikedVideosResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("HasUserLikedVideos")]
        public List<HasUserLikedVideoEntity> HasUserLikedVideos { get; set; }

        public UserServiceHasUserLikedVideosResponseDto(List<HasUserLikedVideoEntity> hasUserLikedVideos, bool success = true)
            : base(success)
        {
            HasUserLikedVideos = hasUserLikedVideos;
        }
    }
}
