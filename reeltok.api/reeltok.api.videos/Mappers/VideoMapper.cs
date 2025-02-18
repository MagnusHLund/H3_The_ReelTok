using reeltok.api.videos.Utils;
using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.DTOs.GetVideosForProfile;

namespace reeltok.api.videos.Mappers
{
    internal static class VideoMapper
    {
        internal static GetVideosForProfileResponseDto ConvertVideoEntityToGetVideosForProfileResponseDto(VideoEntity videoEntity)
        {
            Uri streamUrl = VideoUtils.GetStreamUrl(videoEntity.StreamPath);

            return new GetVideosForProfileResponseDto(
                videoId: videoEntity.VideoId,
                streamUrl: streamUrl,
                uploadedAt: videoEntity.UploadedAt
            );
        }

        internal static VideoEntity ConvertVideoUploadToVideoEntity(VideoUpload videoUpload, Guid videoCreator)
        {
            Guid videoId = Guid.NewGuid();
            string streamPath = VideoUtils.CreateStreamPath(videoCreator, videoId);
            uint currentUnixTime = DateTimeUtils.DateTimeToUnixTime(DateTime.Now);

            return new VideoEntity(
                videoId: videoId,
                userId: videoCreator,
                title: videoUpload.VideoDetails.Title,
                description: videoUpload.VideoDetails.Description,
                tag: videoUpload.VideoDetails.Tag,
                streamPath: streamPath,
                uploadedAt: currentUnixTime
            );
        }
    }
}
