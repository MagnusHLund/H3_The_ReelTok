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

        [HttpGet]
        [Route("feed")]
        public async Task<IActionResult> GetVideosForFeedAsync(
            [FromQuery] Guid userId,
            [FromQuery] byte amount)
        {
            List<VideoForFeedEntity> videos = await _videosService.GetVideosForFeedAsync(userId, amount).ConfigureAwait(false);

            if (videos.Count == 0)
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to get videos for the video feed!");
                return BadRequest(failureResponseDto);
            }

            GetVideosForFeedResponseDto responseDto = new GetVideosForFeedResponseDto(videos);
            return Ok(responseDto);
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetVideosForProfileAsync(
            [FromQuery] Guid userId,
            [FromQuery] uint pageNumber,
            [FromQuery] byte pageSize)
        {
            List<VideoEntity> videos = await _videosService.GetVideosForProfileAsync(
                userId, pageNumber, pageSize).ConfigureAwait(false);

            if (videos.Count == 0)
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to get profile videos!");
                return BadRequest(failureResponseDto);
            }

            GetVideosForProfileResponseDto responseDto = VideoMapper.ConvertVideoEntityToGetVideosForProfileResponseDto(videos);
            return Ok(responseDto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadVideoAsync([FromForm] UploadVideoRequestDto request)
        {
            VideoUpload videoUpload = VideoMapper.ConvertUploadVideoRequestDtoToVideoUpload(request);

            await _videosService.UploadVideoAsync(videoUpload, request.UserId, request.Category).ConfigureAwait(false);

            UploadVideoResponseDto responseDto = new UploadVideoResponseDto();
            return Ok(responseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVideoAsync(
            [FromQuery] Guid userId,
            [FromQuery] Guid videoId)
        {
            await _videosService.DeleteVideoAsync(userId, videoId).ConfigureAwait(false);

            DeleteVideoResponseDto responseDto = new DeleteVideoResponseDto();
            return Ok(responseDto);
        }
    }
}
