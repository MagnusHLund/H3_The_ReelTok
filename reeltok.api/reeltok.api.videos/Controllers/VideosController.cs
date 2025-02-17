using reeltok.api.videos.DTOs;
using Microsoft.AspNetCore.Mvc;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ActionFilters;
using reeltok.api.videos.DTOs.DeleteVideo;

namespace reeltok.api.videos.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        // TODO: Remove unused DTOs
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpGet]
        [Route("GetVideoFeed")]
        public async Task<IActionResult> GetVideosForFeedAsync([FromQuery] Guid userId, [FromQuery] uint pageNumber, [FromQuery] byte pageSize)
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetVideosForProfile")]
        public async Task<IActionResult> GetVideosForProfileAsync([FromQuery] Guid userId, [FromQuery] uint pageNumber, [FromQuery] byte pageSize)
        {
            

            return Ok();
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadVideoAsync([FromBody] UploadVideoRequestDto request)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteVideoAsync([FromQuery] Guid userId, [FromQuery] Guid videoId)
        {
            await _videosService.DeleteVideoAsync(userId, videoId).ConfigureAwait(false);

            DeleteVideoResponseDto responseDto = new DeleteVideoResponseDto();
            return Ok(responseDto);
        }
    }
}
