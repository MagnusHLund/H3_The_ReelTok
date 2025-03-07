using Newtonsoft.Json;
using reeltok.api.videos.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    public class ServiceHasUserLikedVideosResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("HasUserLikedVideos")]
        public List<HasUserLikedVideoEntity> HasUserLikedVideos { get; set; }

        public ServiceHasUserLikedVideosResponseDto(List<HasUserLikedVideoEntity> hasUserLikedVideos, bool success = true)
            : base(success)
        {
            HasUserLikedVideos = hasUserLikedVideos;
        }
    }
}