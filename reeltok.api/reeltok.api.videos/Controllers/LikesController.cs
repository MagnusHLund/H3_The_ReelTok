using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likesService;
        public LikesController(ILikeService likeService) {
            _likesService = likeService;
        }

        [HttpPost]
        [Route("AddLike")]
        public async Task<IActionResult> LikeVideo([FromBody] AddLikeRequestDto request)
        {
            return Ok();
        }

        [HttpPost]
        [Route("RemoveLike")]
        public async Task<IActionResult> RemoveLikeFromVideo([FromBody] RemoveLikeRequestDto request)
        {
            return Ok();
        }
    }
}
