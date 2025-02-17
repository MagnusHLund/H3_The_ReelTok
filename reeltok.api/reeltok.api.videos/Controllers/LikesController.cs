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
        [Route("AddLike")]
        public async Task<IActionResult> LikeVideoAsync([FromBody] AddLikeRequestDto request)
        {
            return Ok();
        }

        [HttpPost]
        [Route("RemoveLike")]
        public async Task<IActionResult> RemoveLikeFromVideoAsync([FromBody] RemoveLikeRequestDto request)
        {
            return Ok();
        }
    }
}
