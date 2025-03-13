using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.Entities.Videos;
using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;
using reeltok.api.gateway.DTOs.Videos.GetVideosForProfile;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetVideosForProfileAsync(
            [FromRoute] Guid userId,
            [FromQuery] int pageNumber,
            [FromQuery] byte pageSize
        )
        {
            List<BaseVideoEntity> videos = await _videosService.GetVideosForProfileAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            GatewayGetVideosForProfileResponseDto responseDto = new GatewayGetVideosForProfileResponseDto(videos);
            return Ok(responseDto);
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetVideosForFeedAsync([FromQuery, Range(1, byte.MaxValue)] byte amount)
        {
            List<VideoForFeedEntity> videos = await _videosService.GetVideosForFeedAsync(amount).ConfigureAwait(false);
            GatewayGetVideosForFeedResponseDto responseDto = new GatewayGetVideosForFeedResponseDto(videos);

            return Ok(responseDto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadVideoAsync([FromBody] GatewayUploadVideoRequestDto request)
        {
            VideoUpload videoUpload = VideoMapper.ConvertRequestDtoToVideoUpload(request);
            bool success = await _videosService.UploadVideoAsync(videoUpload).ConfigureAwait(false);

            GatewayUploadVideoResponseDto responseDto = new GatewayUploadVideoResponseDto(success);
            return Ok(responseDto);
        }

        [HttpPost("{videoId}/like")]
        public async Task<IActionResult> LikeVideoAsync([FromRoute] Guid videoId)
        {
            bool success = await _videosService.LikeVideoAsync(videoId).ConfigureAwait(false);

            GatewayAddLikeResponseDto responseDto = new GatewayAddLikeResponseDto(success);
            return Ok(responseDto);
        }

        [HttpDelete("{videoId}/like")]
        public async Task<IActionResult> RemoveLikeFromVideoAsync([FromRoute] Guid videoId)
        {
            bool success = await _videosService.RemoveLikeFromVideoAsync(videoId).ConfigureAwait(false);

            GatewayRemoveLikeResponseDto responseDto = new GatewayRemoveLikeResponseDto(success);
            return Ok(responseDto);
        }

        [HttpDelete("{videoId}")]
        public async Task<IActionResult> DeleteVideoAsync([FromRoute] Guid videoId)
        {
            bool success = await _videosService.DeleteVideoAsync(videoId).ConfigureAwait(false);

            GatewayDeleteVideoResponseDto responseDto = new GatewayDeleteVideoResponseDto(success);
            return Ok(responseDto);
        }
    }
}
