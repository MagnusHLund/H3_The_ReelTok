using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;
using reeltok.api.gateway.DTOs.Videos.GetVideosForProfile;
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
            GatewayAddLikeResponseDto responseDto = new GatewayAddLikeResponseDto(success);

            return Ok(responseDto);
        }

        [HttpPost]
        [Route("RemoveLike/{videoId}")]
        public async Task<IActionResult> RemoveLikeFromVideo([FromRoute] Guid videoId)
        {
            bool success = await _videosService.RemoveLikeFromVideo(videoId);
            GatewayRemoveLikeResponseDto responseDto = new GatewayRemoveLikeResponseDto(success);

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("GetVideoFeed")]
        public async Task<IActionResult> GetVideosForFeed([FromQuery] byte amount)
        {
            List<Video> videos = await _videosService.GetVideosForFeed(amount);
            GatewayGetVideosForFeedResponseDto responseDto = new GatewayGetVideosForFeedResponseDto(videos);

            return Ok(responseDto);
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadVideo([FromBody] GatewayUploadVideoRequestDto request)
        {
            VideoUpload videoUpload = VideoMapper.ConvertRequestDtoToVideoUpload(request);
            Video video = await _videosService.UploadVideo(videoUpload);
            GatewayUploadVideoResponseDto responseDto = new GatewayUploadVideoResponseDto(video);

            return Ok(responseDto);
        }

        [HttpDelete]
        [Route("Delete/{videoId}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] Guid videoId)
        {
            bool success = await _videosService.DeleteVideo(videoId);
            GatewayDeleteVideoResponseDto responseDto = new GatewayDeleteVideoResponseDto(success);

            return Ok(responseDto);
        }


        [HttpGet]
        [Route("GetVideoForProfile")]
        internal async Task<IActionResult> GetVideosForProfile([FromBody] GatewayGetVideosForProfileRequestDto gatewayGetVideosForProfileRequestDto)
        {
            List<Video> videos = await _videosService.GetVideosForProfile(gatewayGetVideosForProfileRequestDto.UserId, gatewayGetVideosForProfileRequestDto.Amount, gatewayGetVideosForProfileRequestDto.AmountReceived);
            GatewayGetVideosForProfileResponseDto responseDto = new GatewayGetVideosForProfileResponseDto(videos);

            return Ok(responseDto);
        }

    }
}