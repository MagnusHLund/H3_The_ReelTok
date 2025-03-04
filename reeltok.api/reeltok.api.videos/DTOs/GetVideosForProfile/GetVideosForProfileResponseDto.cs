using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.GetVideosForProfile
{
    public class GetVideosForProfileResponseDto
    {
        public List<ProfileVideoEntity> ProfileVideos { get; set; }

        public GetVideosForProfileResponseDto(List<ProfileVideoEntity> profileVideos)
        {
            ProfileVideos = profileVideos;
        }
    }
}
