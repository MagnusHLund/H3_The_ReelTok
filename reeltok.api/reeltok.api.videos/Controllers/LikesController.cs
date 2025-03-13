using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ActionFilters;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;

namespace reeltok.api.videos.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;
        public LikesController(ILikesService likesService)
        {
            _likesService = likesService;
        }

        [HttpPost]
        public async Task<IActionResult> LikeVideoAsync([FromBody] AddLikeRequestDto request)
        {
            bool success = await _likesService.LikeVideoAsync(request.UserId, request.VideoId).ConfigureAwait(false);

            AddLikeResponseDto responseDto = new AddLikeResponseDto(success);
            return Ok(responseDto);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLikeFromVideoAsync([FromQuery] Guid userId, [FromQuery] Guid videoId)
        {
            bool success = await _likesService.LikeVideoAsync(userId, videoId).ConfigureAwait(false);

            RemoveLikeResponseDto responseDto = new RemoveLikeResponseDto(success);
            return Ok(responseDto);
        }
    }
}
