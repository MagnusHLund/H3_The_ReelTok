using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        [HttpPost]
        [Route("AddLike/{videoId}")]
        public async Task<IActionResult> LikeVideo([FromRoute] Guid videoId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("RemoveLike/{videoId}")]
        public async Task<IActionResult> RemoveLikeFromVideo([FromRoute] Guid videoId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("VideoFeed")]
        public async Task<IActionResult> GetVideosForFeed([FromQuery] byte amount)
        {
            return Ok();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadVideo([FromBody] byte amount)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{videoId}")]
        public async Task<IActionResult> DeleteVideo([FromRoute] Guid videoId)
        {
            return Ok();
        }
    }
}