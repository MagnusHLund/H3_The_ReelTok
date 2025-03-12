using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;

namespace reeltok.api.gateway.Mappers
{
    internal static class VideoMapper
    {
        internal static VideoUpload ConvertRequestDtoToVideoUpload(GatewayUploadVideoRequestDto requestDto)
        {
            VideoDetails videoDetails = new VideoDetails(
                requestDto.Title,
                requestDto.Description,
                requestDto.Category
            );

            return new VideoUpload(
                videoDetails: videoDetails,
                videoFile: requestDto.Video
            );
        }

        internal static ServiceUploadVideoRequestDto ConvertVideoUploadToUploadVideoRequestDto(VideoUpload videoUpload)
        {
            return new ServiceUploadVideoRequestDto(
                userId: videoUpload.UserId ?? Guid.Empty,
                title: videoUpload.Title,
                description: videoUpload.Description,
                category: videoUpload.Category,
                videoFile: videoUpload.VideoFile
            );
        }

        internal static VideoEntity ConvertResponseDtoToVideoEntity()
        {

        }
    }
}
