using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpPost]
        [Route("AddLike/{videoId}")]
        public async Task<IActionResult> LikeVideo([FromRoute] Guid videoId)
        {
            bool success = await _videosService.LikeVideo(videoId);
            GatewayAddLikeResponseDto response = new GatewayAddLikeResponseDto(success);

            return Ok(response);
        }

        [HttpPost]
        [Route("RemoveLike/{videoId}")]
        public async Task<IActionResult> RemoveLikeFromVideo([FromRoute] Guid videoId)
        {
            bool success = await _videosService.RemoveLikeFromVideo(videoId);
            GatewayRemoveLikeResponseDto response = new GatewayRemoveLikeResponseDto(success);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetVideoFeed")]
        public async Task<IActionResult> GetVideosForFeed([FromQuery] byte amount)
        {
            List<Video> videos = await _videosService.GetVideosForFeed(amount);
            GatewayGetVideosForFeedResponseDto response = new GatewayGetVideosForFeedResponseDto(videos);

            return Ok(response);
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadVideo([FromBody] GatewayUploadVideoRequestDto request)
        {
            VideoUpload videoUpload = VideoMapper.ConvertRequestDtoToVideoUpload(request);
            Video video = await _videosService.UploadVideo(videoUpload);
            GatewayUploadVideoResponseDto response = new GatewayUploadVideoResponseDto(video);

            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{videoId}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] Guid videoId)
        {
            bool success = await _videosService.DeleteVideo(videoId);
            GatewayDeleteVideoResponseDto response = new GatewayDeleteVideoResponseDto(success);

            return Ok(response);
        }
    }
}