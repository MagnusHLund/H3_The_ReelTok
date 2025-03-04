namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    public class ServiceUserLikedVideosRequestDto
    {
        public Guid UserId { get; set; }
        public List<Guid> VideoIds { get; set; }

        public ServiceUserLikedVideosRequestDto(Guid userId, List<Guid> videoIds)
        {
            UserId = userId;
            VideoIds = videoIds;
        }
    }
}