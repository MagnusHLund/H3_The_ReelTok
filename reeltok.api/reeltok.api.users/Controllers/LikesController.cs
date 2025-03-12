using Microsoft.AspNetCore.Mvc;
using reeltok.api.users.Mappers;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.ActionFilters;
using reeltok.api.users.DTOs.LikeVideo;
using reeltok.api.users.DTOs.RemoveLike;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.DTOs.GetHasLikedVideoAsync;

namespace reeltok.api.users.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likeVideoService;

        public LikesController(ILikesService likeVideoService)
        {
            _likeVideoService = likeVideoService;
        }

        // Called from Video API
        [HttpGet]
        public async Task<IActionResult> GetHasUserLikedVideosAsync([FromQuery] Guid userId, [FromQuery] List<Guid> videoIds)
        {
            // TODO: Implement this! Follow the same code style, as elsewhere in the codebase.

            //HasUserLikedVideosResponseDto response = new HasUserLikedVideosResponseDto();
            return Ok(/* response */);
        }

        // Called from Video API
        [HttpPost]
        public async Task<IActionResult> AddLikeToVideoAsync([FromBody] LikeVideoRequestDto request)
        {
            LikedDetails likedDetails = LikesMapper.ConvertLikeVideoRequestDtoToLikedDetails(request);

            bool success = await _likeVideoService.AddToLikedVideosAsync(likedDetails)
                .ConfigureAwait(false);

            LikeVideoResponseDto response = new LikeVideoResponseDto(success);
            return Ok(response);
        }

        // Called from Video API
        [HttpDelete]
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
