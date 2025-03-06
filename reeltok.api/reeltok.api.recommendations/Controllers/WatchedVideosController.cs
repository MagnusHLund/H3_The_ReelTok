using reeltok.api.auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.ValueObjects;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchedVideosController : ControllerBase
    {
        private readonly IWatchedVideoService _watchedVideoService;

        public WatchedVideosController(IWatchedVideoService watchedVideoService)
        {
            _watchedVideoService = watchedVideoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddWatchedVideoAsync([FromBody] CreateWatchedVideoDto createWatchedVideoDto)
        {
            try
            {
                WatchedVideoDetails watchedVideoDetails = WatchedVideosMapper.ToEntity(createWatchedVideoDto);
                WatchedVideoEntity watchedVideoEntity = new WatchedVideoEntity(watchedVideoDetails);
                bool IsAdded = await _watchedVideoService.AddWatchedVideoAsync(watchedVideoEntity);
                return Ok(IsAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(new FailureResponseDto(ex.Message));
            }
        }

        [HttpPut("update-time-watched")]
        public async Task<IActionResult> UpdateWatchedTime([FromBody] UpdateWatchedTimeDto dto)
        {
            try
            {
                (bool updated, string message) = await _watchedVideoService.UpdateTimeWatchedAsync(dto.VideoId, dto.UserId);

                if (updated != true)
                {
                    return BadRequest(new FailureResponseDto(message));
                }
                else
                {
                    return Ok(updated);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new FailureResponseDto(ex.Message));
            }
        }
    }
}
