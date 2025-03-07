using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Mappers;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.DTOs.LikeVideo;
using reeltok.api.users.DTOs.RemoveLike;
using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class LikeVideosController : ControllerBase
    {
        private readonly ILikeVideosService _likeVideoService;

        public LikeVideosController(ILikeVideosService likeVideoService)
        {
            _likeVideoService = likeVideoService;
        }

        // Called from Video API
        [HttpPost("like")]
        public async Task<IActionResult> AddLikeToVideoAsync([FromBody] LikeVideoRequestDto request)
        {
            LikedDetails likedDetails = LikeVideoMapper.ToLikeVideoFromCreateDTO(request);

            bool success = await _likeVideoService.AddToLikedVideosAsync(likedDetails)
                .ConfigureAwait(false);

            LikeVideoResponseDto response = new LikeVideoResponseDto(success);
            return Ok(response);
        }

        // Called from Video API
        [HttpDelete("like")]
        public async Task<IActionResult> RemoveLikeFromVideoAsync([FromQuery] Guid videoId, [FromQuery] Guid userId)
        {
            LikedDetails likeVideo = new LikedDetails(userId, videoId);

            bool success = await _likeVideoService.RemoveFromLikedVideosAsync(likeVideo.UserId, likeVideo.VideoId)
                .ConfigureAwait(false);

            RemoveLikeResponseDto response = new RemoveLikeResponseDto(success);
            return Ok(response);
        }
    }
}
