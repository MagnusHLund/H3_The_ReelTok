using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.ActionFilters;
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
        internal async Task<IActionResult> GetVideosForProfileAsync([FromBody] GatewayGetVideosForProfileRequestDto gatewayGetVideosForProfileRequestDto)
        {
            List<Video> videos = await _videosService.GetVideosForProfile(gatewayGetVideosForProfileRequestDto.UserId, gatewayGetVideosForProfileRequestDto.Amount, gatewayGetVideosForProfileRequestDto.AmountReceived).ConfigureAwait(false);
            GatewayGetVideosForProfileResponseDto responseDto = new GatewayGetVideosForProfileResponseDto(videos);

            return Ok(responseDto);
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetVideosForFeedAsync([FromQuery] byte amount)
        {
            List<Video> videos = await _videosService.GetVideosForFeed(amount).ConfigureAwait(false);
            GatewayGetVideosForFeedResponseDto responseDto = new GatewayGetVideosForFeedResponseDto(videos);

            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideoAsync([FromBody] GatewayUploadVideoRequestDto request)
        {
            VideoUpload videoUpload = VideoMapper.ConvertRequestDtoToVideoUpload(request);
            Video video = await _videosService.UploadVideo(videoUpload).ConfigureAwait(false);
            GatewayUploadVideoResponseDto responseDto = new GatewayUploadVideoResponseDto(video);

            return Ok(responseDto);
        }

        [HttpPost("{videoId}/like")]
        public async Task<IActionResult> LikeVideoAsync([FromRoute] Guid videoId)
        {
            bool success = await _videosService.LikeVideo(videoId).ConfigureAwait(false);
            GatewayAddLikeResponseDto responseDto = new GatewayAddLikeResponseDto(success);

            return Ok(responseDto);
        }

        [HttpDelete("{videoId}/like")]
        public async Task<IActionResult> RemoveLikeFromVideoAsync([FromRoute] Guid videoId)
        {
            bool success = await _videosService.RemoveLikeFromVideo(videoId).ConfigureAwait(false);
            GatewayRemoveLikeResponseDto responseDto = new GatewayRemoveLikeResponseDto(success);

            return Ok(responseDto);
        }

        [HttpDelete("{videoId}")]
        public async Task<IActionResult> DeleteVideoAsync([FromRoute] Guid videoId)
        {
            bool success = await _videosService.DeleteVideo(videoId).ConfigureAwait(false);
            GatewayDeleteVideoResponseDto responseDto = new GatewayDeleteVideoResponseDto(success);

            return Ok(responseDto);
        }
    }
}
