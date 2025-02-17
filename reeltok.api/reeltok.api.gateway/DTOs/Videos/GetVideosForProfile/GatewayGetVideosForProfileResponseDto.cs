using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    internal class GatewayGetVideosForProfileResponseDto : BaseResponseDto
    {
        internal List<Video> Videos { get; set; }


        internal GatewayGetVideosForProfileResponseDto(List<Video> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }

        internal GatewayGetVideosForProfileResponseDto()
        {
        }

    }
}
