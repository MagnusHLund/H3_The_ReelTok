using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;
using reeltok.api.recommendations.ActionFilters;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.DTOs.AddVideoRecommendation;
using reeltok.api.recommendations.DTOs.GetRecommendedVideosForUsersFeed;
using reeltok.api.recommendations.DTOs.UpdateTotalTimesUserWatchedAVideo;

namespace reeltok.api.recommendations.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class VideoRecommendationsController : ControllerBase
    {
        private readonly IVideoRecommendationService _videoRecommendationService;

        public VideoRecommendationsController(IVideoRecommendationService videoRecommendationService)
        {
            _videoRecommendationService = videoRecommendationService;
        }

        // Called from Videos api
        [HttpGet]
        public async Task<IActionResult> GetRecommendedVideosForUsersFeedAsync([FromQuery] Guid userId, [FromQuery] byte amount)
        {
            // TODO: If amount is higher than 20, make the amount 20

            List<Guid> videoIds = await _recommendationsService.GetTopVideoByUserInterestAsync(userId, amount)
                .ConfigureAwait(false);

            GetRecommendedVideosForUsersFeedResponseDto response = new GetRecommendedVideosForUsersFeedResponseDto(videoIds);
            return Ok(response);
        }

        // Called from Videos api
        [HttpPost]
        public async Task<IActionResult> AddVideoRecommendationAsync([FromBody] AddVideoRecommendationRequestDto request)
        {
            VideoCategoryDetails videoCategoryDetails = VideoRecommendationMapper.ToVideoCategoryDetailsFromDTO(request);

            VideoCategoryEntity videoCategory = new VideoCategoryEntity(videoCategoryDetails);

            bool isAdded = await _videoRecommendationService.AddRecommendationForVideoAsync(videoCategory, request.CategoryId);

            if (!isAdded)
            {
                return BadRequest(new FailureResponseDto("Failed to add video recommendation"));
            }

            AddVideoRecommendationResponseDto response = new AddVideoRecommendationResponseDto();
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTotalTimesUserWatchedAVideoAsync(
            [FromBody] UpdateTotalTimesUserWatchedVideosRequestDto request)
        {

            WatchedVideoDetails watchedVideoDetails = WatchedVideosMapper.ToEntity(createWatchedVideoDto);
            WatchedVideoEntity watchedVideoEntity = new WatchedVideoEntity(watchedVideoDetails);
            (bool result, string message) = await _watchedVideoService.AddOrUpdateWatchedVideoAsync(watchedVideoEntity);

            if (!result)
            {
                return BadRequest(new FailureResponseDto(message));
            }

            UpdateTotalTimesUserWatchedVideosResponseDto response = new UpdateTotalTimesUserWatchedVideosResponseDto();
            return Ok(response);
        }
    }
}
