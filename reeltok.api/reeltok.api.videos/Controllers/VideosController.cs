using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
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

        [HttpGet]
        [Route("GetVideoFeed")]
        public async Task<IActionResult> GetVideosForFeed([FromBody] GetVideosForFeedRequestDto request)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadVideo([FromBody] UploadVideoRequestDto request)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteVideo([FromBody] DeleteVideoRequestDto request)
        {
            return Ok();
        }
    }
}