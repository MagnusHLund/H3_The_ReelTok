using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.Utils;
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
        private readonly IAuthService _authService;

        public VideosController(IVideosService videosService, IAuthService authService)
        {
            _videosService = videosService;
            _authService = authService;
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetVideosForProfileAsync(
            [FromRoute] Guid userId,
            [FromQuery, Range(0, int.MaxValue)] int pageNumber = 0,
            [FromQuery, Range(1, byte.MaxValue)] byte pageSize = 15
        )
        {
            List<BaseVideoUsingDateTimeEntity> videos = await _videosService
                .GetVideosForProfileAsync(userId, pageNumber, pageSize)
                .ConfigureAwait(false);

            GatewayGetVideosForProfileResponseDto responseDto = new GatewayGetVideosForProfileResponseDto(videos);
            return Ok(responseDto);
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetVideosForFeedAsync([FromQuery, Range(1, byte.MaxValue)] byte amount = 3)
        {
            // This endpoint can use UserId, but its not mandatory.
            // We fetch it, if there are cookies present, else we provide an empty guid.

            Guid userId = Guid.Empty;

            if (CookieUtils.HasCookie(HttpContext, "AccessToken") && CookieUtils.HasCookie(HttpContext, "RefreshToken"))
            {
                userId = await _authService.GetUserIdByAccessTokenAsync().ConfigureAwait(false);
            }

            List<VideoForFeedUsingDateTimeEntity> videos = await _videosService
                .GetVideosForFeedAsync(amount, userId)
                .ConfigureAwait(false);

            GatewayGetVideosForFeedResponseDto responseDto = new GatewayGetVideosForFeedResponseDto(videos);
            return Ok(responseDto);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadVideoAsync([FromForm] GatewayUploadVideoRequestDto request)
        {
            if (!CategoryValidationUtils.IsValidCategoryType(request.Category))
            {
                throw new ArgumentException("Invalid category type!");
            }

            VideoUpload videoUpload = VideoMapper.ConvertRequestDtoToVideoUpload(request);
            BaseVideoUsingDateTimeEntity uploadedVideo = await _videosService.UploadVideoAsync(videoUpload)
                .ConfigureAwait(false);

            GatewayUploadVideoResponseDto responseDto = new GatewayUploadVideoResponseDto(uploadedVideo);
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
