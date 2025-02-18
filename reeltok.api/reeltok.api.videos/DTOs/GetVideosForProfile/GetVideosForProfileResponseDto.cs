namespace reeltok.api.videos.DTOs.GetVideosForProfile
{
    public class GetVideosForProfileResponseDto
    {
        public Guid VideoId { get; set; }
        public Uri StreamUrl { get; set; }
        public uint UploadedAt { get; set; }

        public GetVideosForProfileResponseDto(Guid videoId, Uri streamUrl, uint uploadedAt)
        {
            VideoId = videoId;
            StreamUrl = streamUrl;
            UploadedAt = uploadedAt;
        }
    }
}
