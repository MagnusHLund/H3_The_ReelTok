using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    public class ServiceGetVideosForProfileResponseDto : BaseResponseDto
    {
        public List<BaseVideoEntity> Videos { get; set; }

        public ServiceGetVideosForProfileResponseDto(List<BaseVideoEntity> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
