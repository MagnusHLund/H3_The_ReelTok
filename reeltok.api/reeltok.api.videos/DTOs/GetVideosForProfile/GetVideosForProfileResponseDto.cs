namespace reeltok.api.videos.DTOs.GetVideosForProfile
{
    public class GetVideosForProfileResponseDto
    {
        public Guid VideoId { get; set; }
        public Uri StreamUrl { get; set; }
        public uint UploadDate { get; set; }

        public GetVideosForProfileResponseDto(Guid videoId, Uri streamUrl, uint uploadDate)
        {
            VideoId = videoId;
            StreamUrl = streamUrl;
            UploadDate = uploadDate;
        }
    }
}
