using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Videos;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    public class GatewayGetVideosForProfileResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Videos")]
        public List<BaseVideoUsingDateTimeEntity> Videos { get; set; }

        public GatewayGetVideosForProfileResponseDto(
            List<BaseVideoUsingDateTimeEntity> videos,
            bool success = true
        ) : base(success)
        {
            Videos = videos;
        }
    }
}
