using reeltok.api.videos.DTOs.GetVideosForProfile;
using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Mappers
{
    internal static class VideoMapper
    {
        internal static GetVideosForProfileResponseDto ConvertVideoEntityToGetVideosForProfileResponseDto(VideoEntity videoEntity)
        {
            Uri streamUrl = new Uri(videoEntity.StreamUrl);

            return new GetVideosForProfileResponseDto(
                videoId: videoEntity.VideoId,
                streamUrl: videoEntity.StreamUrl,

            );
        }
    }
}
