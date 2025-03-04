namespace reeltok.api.videos.DTOs.GetUserDetailsForVideo
{
    public class ServiceGetUserDetailsForVideoRequestDto
    {
        public List<Guid> VideoIds { get; set; }

        public ServiceGetUserDetailsForVideoRequestDto(List<Guid> videoIds)
        {
            VideoIds = videoIds;
        }

        public ServiceGetUserDetailsForVideoRequestDto() { }
    }
}
