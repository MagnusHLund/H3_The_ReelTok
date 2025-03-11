using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    public class UsersServiceHasUserLikedVideosResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("HasUserLikedVideos")]
        public List<HasUserLikedVideoEntity> HasUserLikedVideos { get; set; }

        public UsersServiceHasUserLikedVideosResponseDto(List<HasUserLikedVideoEntity> hasUserLikedVideos, bool success = true)
            : base(success)
        {
            HasUserLikedVideos = hasUserLikedVideos;
        }
    }
}
