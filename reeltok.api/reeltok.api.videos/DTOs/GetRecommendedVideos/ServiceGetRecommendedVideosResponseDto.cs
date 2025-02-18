namespace reeltok.api.videos.DTOs.GetRecommendedVideos
{
    public class ServiceGetRecommendedVideosResponseDto : BaseResponseDto
    {
        public List<Guid> VideoIdList { get; set; }

        public ServiceGetRecommendedVideosResponseDto(List<Guid> videoIdList, bool success = true) : base(success)
        {
            VideoIdList = videoIdList;
        }
        public ServiceGetRecommendedVideosResponseDto() { }
    }
}
