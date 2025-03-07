using reeltok.api.videos.DTOs;
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
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;
        public LikesController(ILikesService likesService)
        {
            _likesService = likesService;
        }

        [HttpPost]
        [Route("like")]
        public async Task<IActionResult> LikeVideoAsync([FromBody] AddLikeRequestDto request)
        {
            bool success = await _likesService.LikeVideoAsync(request.UserId, request.VideoId).ConfigureAwait(false);

            if (!success)
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to like the video!");
                return BadRequest(failureResponseDto);
            }

            AddLikeResponseDto responseDto = new AddLikeResponseDto();
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("remove-like")]
        public async Task<IActionResult> RemoveLikeFromVideoAsync([FromBody] RemoveLikeRequestDto request)
        {
            bool success = await _likesService.LikeVideoAsync(request.UserId, request.VideoId).ConfigureAwait(false);

            if (!success)
            {
                FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to remove like from the video!");
                return BadRequest(failureResponseDto);
            }

            RemoveLikeResponseDto responseDto = new RemoveLikeResponseDto();
            return Ok(responseDto);
        }
    }
}
