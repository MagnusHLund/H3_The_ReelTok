using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class UserServiceGetUserDetailsForVideoRequestDto
    {
        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public UserServiceGetUserDetailsForVideoRequestDto(List<Guid> videoIds)
        {
            VideoIds = videoIds;
        }
    }
}
