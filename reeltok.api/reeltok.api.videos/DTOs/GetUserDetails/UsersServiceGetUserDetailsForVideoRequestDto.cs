using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetails
{
    public class UsersServiceGetUserDetailsForVideoRequestDto
    {
        [Required]
        [JsonProperty("UserIds")]
        public List<Guid> UserIds { get; set; }

        public UsersServiceGetUserDetailsForVideoRequestDto(List<Guid> userIds)
        {
            UserIds = userIds;
        }
    }
}
