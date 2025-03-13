using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;

namespace reeltok.api.gateway.Mappers
{
    internal static class VideoMapper
    {
        internal static VideoUpload ConvertRequestDtoToVideoUpload(GatewayUploadVideoRequestDto requestDto)
        {
            return new VideoUpload(
                title: requestDto.Title,
                description: requestDto.Description,
                category: requestDto.Category,
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
    }
}
