using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.DTOs;
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
