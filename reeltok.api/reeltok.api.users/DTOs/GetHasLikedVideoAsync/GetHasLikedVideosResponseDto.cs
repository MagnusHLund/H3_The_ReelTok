using Newtonsoft.Json;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.GetHasLikedVideoAsync
{
    public class HasUserLikedVideosResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("HasUserLikedVideos")]
        public List<HasUserLikedVideoEntity> HasUserLikedVideos { get; set; }

        public HasUserLikedVideosResponseDto(List<HasUserLikedVideoEntity> hasUserLikedVideos, bool success = true)
            : base(success)
        {
            HasUserLikedVideos = hasUserLikedVideos;
        }
    }
}