using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    internal class VideosController : ControllerBase
    {
        private readonly IVideosService _videosService;

        internal VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpPost]
        [Route("AddLike")]
        internal async Task<IActionResult> LikeVideo([FromBody] AddLikeRequestDto request)
        {
            return Ok();
        }

        [HttpPost]
        [Route("RemoveLike")]
        internal async Task<IActionResult> RemoveLikeFromVideo([FromBody] RemoveLikeRequestDto request)
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetVideoFeed")]
        internal async Task<IActionResult> GetVideosForFeed([FromBody] GetVideosForFeedRequestDto request)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Upload")]
        internal async Task<IActionResult> UploadVideo([FromBody] UploadVideoRequestDto request)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        internal async Task<IActionResult> DeleteVideo([FromBody] DeleteVideoRequestDto request)
        {
            return Ok();
        }
    }
}
