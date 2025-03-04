using reeltok.api.videos.Utils;
using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.DTOs.GetVideosForProfile;
using reeltok.api.videos.DTOs;

namespace reeltok.api.videos.Mappers
{
    internal static class VideoMapper
    {
        internal static GetVideosForProfileResponseDto ConvertVideoEntityToGetVideosForProfileResponseDto(
            List<VideoEntity> videoEntity)
        {
            List<ProfileVideoEntity> profileVideos = new List<ProfileVideoEntity>();

            foreach (VideoEntity video in videoEntity)
            {
                ProfileVideoEntity profileVideo = new ProfileVideoEntity(
                    videoId: video.VideoId,
                    streamPath: video.StreamPath,
                    uploadedAt: video.UploadedAt
                );

                profileVideos.Add(profileVideo);
            }

            return new GetVideosForProfileResponseDto(
                profileVideos: profileVideos
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

        internal static VideoUpload ConvertUploadVideoRequestDtoToVideoUpload(UploadVideoRequestDto requestDto)
        {
            VideoDetails videoDetails = new VideoDetails(
                title: requestDto.Title,
                description: requestDto.Description,
                tag: 0
            );

            return new VideoUpload(
                videoDetails: videoDetails,
                videoFile: requestDto.VideoFile);
        }
    }
}
