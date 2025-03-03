using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class ServiceGetUserDetailsForVideoResponseDto : BaseResponseDto
    {
        public List<VideoCreatorEntity> VideoCreatorDetailsList { get; set; }

        public ServiceGetUserDetailsForVideoResponseDto(List<VideoCreatorEntity> videoCreatorDetailsList, bool success) : base(success)
        {
            VideoCreatorDetailsList = videoCreatorDetailsList;
        }

        public ServiceGetUserDetailsForVideoResponseDto() { }
    }
}
