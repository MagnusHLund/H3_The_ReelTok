using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    public class ServiceGetVideosForProfileResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<BaseVideoEntity> Videos { get; set; }

        public ServiceGetVideosForProfileResponseDto(List<BaseVideoEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
