using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Mappers
{
    internal static class VideoMapper
    {
        internal static VideoUpload ConvertRequestDtoToVideoUpload(GatewayUploadVideoRequestDto requestDto)
        {
            VideoDetails videoDetails = new VideoDetails(
                requestDto.Title,
                requestDto.Description,
                requestDto.Tag
                );

            return new VideoUpload(
                videoDetails: videoDetails,
                videoFile: requestDto.Video
                );
        }
    }
}