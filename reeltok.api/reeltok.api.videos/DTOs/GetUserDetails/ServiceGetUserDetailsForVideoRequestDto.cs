using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class ServiceGetUserDetailsForVideoRequestDto
    {
        [Required]
        [JsonProperty("VideoIds")]
        public List<Guid> VideoIds { get; set; }

        public ServiceGetUserDetailsForVideoRequestDto(List<Guid> videoIds)
        {
            VideoIds = videoIds;
        }
    }
}
