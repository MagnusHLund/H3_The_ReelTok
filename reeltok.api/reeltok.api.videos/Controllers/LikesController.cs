using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Services;

namespace reeltok.api.videos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService _likesService;
        public LikesController(ILikesService likesService) {
            _likesService = likesService;
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
