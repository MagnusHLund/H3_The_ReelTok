using reeltok.api.auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;

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

        [HttpPut]
        public async Task<IActionResult> AddOrUpdateWatchedVideoAsync([FromBody] CreateWatchedVideoDto createWatchedVideoDto)
        {
            try
            {
                WatchedVideoDetails watchedVideoDetails = WatchedVideosMapper.ToEntity(createWatchedVideoDto);
                WatchedVideoEntity watchedVideoEntity = new WatchedVideoEntity(watchedVideoDetails);
                (bool result, string message) = await _watchedVideoService.AddOrUpdateWatchedVideoAsync(watchedVideoEntity);

                if (!result)
                {
                    return BadRequest(new FailureResponseDto(message));
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(new FailureResponseDto(ex.Message));
            }
        }
    }
}
