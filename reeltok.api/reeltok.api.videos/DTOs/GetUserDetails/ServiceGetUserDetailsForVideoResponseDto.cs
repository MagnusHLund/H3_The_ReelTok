using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class ServiceGetUserDetailsForVideoResponseDto : BaseResponseDto
    {
        public List<VideoCreatorDetails> VideoCreatorDetailsList { get; set; }

        public ServiceGetUserDetailsForVideoResponseDto(List<VideoCreatorDetails> videoCreatorDetailsList, bool success) : base(success)
        {
            VideoCreatorDetailsList = videoCreatorDetailsList;
        }

        public ServiceGetUserDetailsForVideoResponseDto() { }
    }
}
