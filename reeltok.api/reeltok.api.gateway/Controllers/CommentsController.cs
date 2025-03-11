using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Entities.comments;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.DTOs.Comments.AddComment;
using reeltok.api.gateway.DTOs.Comments.LoadComments;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> LoadCommentsAsync(
            [FromRoute] Guid videoId,
            [FromQuery, Range(1, int.MaxValue)] int pageNumber = 15,
            [FromQuery, Range(1, byte.MaxValue)] byte pageSize = 15
            )
        {
            List<CommentUsingDateTime> comments = await _commentsService.LoadCommentsAsync(videoId, pageNumber, pageSize)
                .ConfigureAwait(false);

            GatewayLoadCommentsResponseDto responseDto = new GatewayLoadCommentsResponseDto(comments);

            return Ok(responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddCommentAsync([FromBody] GatewayAddCommentRequestDto request)
        {
            CommentUsingDateTime comment = await _commentsService.AddCommentAsync(request.VideoId, request.Message)
                .ConfigureAwait(false);

            GatewayAddCommentResponseDto responseDto = new GatewayAddCommentResponseDto(comment);
            return Ok(responseDto);
        }
    }
}
