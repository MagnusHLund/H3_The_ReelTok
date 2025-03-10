using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.ActionFilters;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.DTOs.AddVideoCategory;
using reeltok.api.recommendations.DTOs.GetRecommendedVideosForUsersFeed;
using reeltok.api.recommendations.DTOs.UpdateTotalTimesUserWatchedAVideo;

namespace reeltok.api.recommendations.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;
        private readonly IWatchedVideosService _watchedVideosService;

        public VideosController(IVideosService videosService, IWatchedVideosService watchedVideosService)
        {
            _videosService = videosService;
            _watchedVideosService = watchedVideosService;
        }

        // Called from Videos api
        [HttpGet]
        public async Task<IActionResult> GetRecommendedVideosForUsersFeedAsync([FromQuery] Guid userId, [FromQuery] byte amount)
        {
            List<Guid> videoIds = await _videosService.GetRecommendedVideosForUsersFeedAsync(userId, amount)
                .ConfigureAwait(false);

            GetRecommendedVideosForUsersFeedResponseDto response = new GetRecommendedVideosForUsersFeedResponseDto(videoIds);
            return Ok(response);
        }

        // Called from Videos api
        [HttpPost]
        public async Task<IActionResult> AddVideoCategoryAsync([FromBody] AddVideoCategoryRequestDto request)
        {
            CategoryType videoCategory = await _videosService.AddVideoCategoryAsync(request.VideoId, request.Category)
                .ConfigureAwait(false);

            AddVideoCategoryResponseDto response = new AddVideoCategoryResponseDto(videoCategory);
            return Ok(response);
        }

        [HttpPut("watched")]
        public async Task<IActionResult> UpdateTotalTimesUserWatchedVideosAsync(
            [FromBody] UpdateTotalTimesUserWatchedVideosRequestDto request)
        {
            await _watchedVideosService.UpdateTotalTimesUserWatchedVideosAsync(request.UserId, request.VideoIds)
                .ConfigureAwait(false);

            UpdateTotalTimesUserWatchedVideosResponseDto response = new UpdateTotalTimesUserWatchedVideosResponseDto();
            return Ok(response);
        }
    }
}
