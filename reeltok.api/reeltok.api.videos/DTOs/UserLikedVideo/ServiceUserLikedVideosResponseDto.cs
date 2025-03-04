using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    public class ServiceUserLikedVideosResponseDto : BaseResponseDto
    {
        public List<HasUserLikedVideoEntity> HasUserLikedVideos { get; set; }

        public ServiceUserLikedVideosResponseDto(List<HasUserLikedVideoEntity> hasUserLikedVideos)
        {
            HasUserLikedVideos = hasUserLikedVideos;
        }

        public ServiceUserLikedVideosResponseDto() { }
    }
}