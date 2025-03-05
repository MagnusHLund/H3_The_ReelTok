using Microsoft.AspNetCore.Mvc;
using reeltok.api.auth.DTOs;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Controllers
{
    [ApiController]
    [Route("api/Video recommendations")]
    public class VideoRecommendationController : ControllerBase
    {
        private readonly IVideoRecommendationService _videoRecommendationService;

        public VideoRecommendationController(IVideoRecommendationService videoRecommendationService)
        {
            _videoRecommendationService = videoRecommendationService;
        }

        [HttpPost("Add video recommendation")]
        public async Task<IActionResult> AddVideoRecommendationAsync([FromBody] CreateVideoInterestDto dto)
        {
            VideoCategoryDetails videoCategoryDetails = VideoRecommendationMapper.ToVideoCategoryDetailsFromDTO(dto);

            VideoCategoryEntity videoCategory = new VideoCategoryEntity(videoCategoryDetails);

            bool isAdded = await _videoRecommendationService.AddRecommendationForVideoAsync(videoCategory, dto.CategoryId);

            if (!isAdded)
            {
                return BadRequest(new FailureResponseDto("Failed to add video recommendation"));
            }

            return Ok(isAdded);
        }

        [HttpGet("Get video recommendation")]
        public async Task<IActionResult> GetVideoRecommendationAsync([FromQuery] Guid videoId)
        {
            VideoCategoryEntity? videoCategory = await _videoRecommendationService. GetVideoCategoryAsync(videoId);

            if (videoCategory == null)
            {
                return BadRequest(new FailureResponseDto("Video not found"));
            }

            if (videoCategory.Categories == null || !videoCategory.Categories.Any())
            {
                return BadRequest(new FailureResponseDto("Category not found"));
            }

            CategoryEntity category = videoCategory.Categories.First();

            VideoCategoryResponseDto videoCategoryDto = VideoRecommendationMapper.ToVideoCategoryDTOFromEntity(videoCategory, category);

            return Ok(videoCategoryDto);
        }
    }
}
