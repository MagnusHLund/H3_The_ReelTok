using reeltok.api.videos.DTOs;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.Mappers;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.ActionFilters;
using reeltok.api.videos.DTOs.DeleteVideo;
using reeltok.api.videos.DTOs.UploadVideo;
using reeltok.api.videos.DTOs.GetVideosForFeed;
using reeltok.api.videos.DTOs.GetVideosForProfile;
using reeltok.api.videos.DTOs.GetVideoById;

namespace reeltok.api.videos.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        // Called by Comments api
        [HttpGet]
        public async Task<IActionResult> GetVideoByIdAsync([FromQuery] Guid VideoId)
        {
            VideoEntity video = await _videosService.GetVideoByIdAsync(VideoId).ConfigureAwait(false);

            GetVideoByIdResponseDto responseDto = new GetVideoByIdResponseDto(video);
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("feed")]
        public async Task<IActionResult> GetVideosForFeedAsync(
            [FromQuery] byte amount,
            [FromQuery] Guid? userId = null
        )
        {
            Guid userIdOrDefault = userId ?? Guid.Empty;
            List<VideoForFeedEntity> videos = await _videosService.GetVideosForFeedAsync(userIdOrDefault, amount)
                .ConfigureAwait(false);

            GetVideosForFeedResponseDto responseDto = new GetVideosForFeedResponseDto(videos);
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetVideosForProfileAsync(
            [FromQuery] Guid userId,
            [FromQuery] uint pageNumber,
            [FromQuery] byte pageSize
        )
        {
            List<VideoEntity> videos = await _videosService.GetVideosForProfileAsync(
                userId, pageNumber, pageSize).ConfigureAwait(false);

            GetVideosForProfileResponseDto responseDto = VideoMapper.ConvertVideoEntityToGetVideosForProfileResponseDto(videos);
            return Ok(responseDto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadVideoAsync([FromForm] UploadVideoRequestDto request)
        {
            VideoUpload videoUpload = VideoMapper.ConvertUploadVideoRequestDtoToVideoUpload(request);
            byte category = (byte)FormDataMapper.ConvertStringToint(request.Category);
            Guid userId = FormDataMapper.ConvertStringToGuid(request.UserId);

            VideoEntity uploadedVideo = await _videosService.UploadVideoAsync(videoUpload, userId, category)
                .ConfigureAwait(false);

            UploadVideoResponseDto responseDto = new UploadVideoResponseDto(uploadedVideo);
            return Ok(responseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVideoAsync(
            [FromQuery] Guid userId,
            [FromQuery] Guid videoId
        )
        {
            await _videosService.DeleteVideoAsync(userId, videoId).ConfigureAwait(false);

            DeleteVideoResponseDto responseDto = new DeleteVideoResponseDto();
            return Ok(responseDto);
        }
    }
}
