using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class UsersServiceGetUserDetailsForVideoRequestDto
    {
        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public UsersServiceGetUserDetailsForVideoRequestDto(List<Guid> videoIds)
        {
            VideoIds = videoIds;
        }
    }
}
