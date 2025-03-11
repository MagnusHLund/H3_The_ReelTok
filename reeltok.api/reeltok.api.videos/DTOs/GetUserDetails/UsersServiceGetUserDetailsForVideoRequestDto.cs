using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetails
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
