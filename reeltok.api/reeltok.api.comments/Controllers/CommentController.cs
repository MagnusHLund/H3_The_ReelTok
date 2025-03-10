using Microsoft.AspNetCore.Mvc;
using reeltok.api.comments.Entities;
using reeltok.api.comments.ActionFilters;
using reeltok.api.comments.DTOs.CreateComment;
using reeltok.api.comments.Interfaces.Services;
using reeltok.api.comments.DTOs.GetCommentsByVideoId;

namespace reeltok.api.comments.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        public CommentController(ICommentsService commentService)
        {
            _commentsService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentsByVideoIdAsync(
            [FromQuery] Guid videoId,
            [FromQuery] int pageNumber,
            [FromQuery] byte pageSize
        )
        {
            List<CommentEntity> comments = await _commentsService.GetCommentsByVideoIdAsync(videoId, pageNumber, pageSize)
                .ConfigureAwait(false);

            int totalVideoComments = await _commentsService.GetTotalCommentsForVideoAsync(videoId)
                .ConfigureAwait(false);

            GetCommentsByVideoIdResponseDto response = new GetCommentsByVideoIdResponseDto(totalVideoComments, comments);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentRequestDto request)
        {
            CommentEntity comment = await _commentsService.CreateCommentAsync(request.VideoId, request.UserId, request.CommentText)
                .ConfigureAwait(false);

            CreateCommentResponseDto response = new CreateCommentResponseDto(comment);
            return Ok(response);
        }
    }
}
