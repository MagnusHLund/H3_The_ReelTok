using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    public class GatewayGetVideosForProfileResponseDto : BaseResponseDto
    {
        public List<BaseVideoEntity> Videos { get; set; }

        public GatewayGetVideosForProfileResponseDto(List<BaseVideoEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
