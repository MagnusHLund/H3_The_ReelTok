using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ActionFilters;
using System.ComponentModel.DataAnnotations;
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

        [HttpPost]
        public async Task<IActionResult> AddCommentAsync([FromBody] GatewayAddCommentRequestDto request)
        {

            CommentUsingDateTime comment = await _commentsService.AddComment(request.VideoId, request.CommentText)
                .ConfigureAwait(false);

            GatewayAddCommentResponseDto responseDto = new GatewayAddCommentResponseDto(comment);
            return Ok(responseDto);
        }

        [HttpGet("{videoId}")]
        public async Task<IActionResult> LoadCommentsAsync(
            [FromRoute] Guid videoId,
            [FromQuery, Range(1, byte.MaxValue)] byte amount = 15
        )
        {
            List<CommentUsingDateTime> comments = await _commentsService.LoadComments(videoId, amount).ConfigureAwait(false);

            if (comments.Count.Equals(0))
            {
                return NoContent();
            }

            GatewayLoadCommentsResponseDto responseDto = new GatewayLoadCommentsResponseDto(comments);

            return Ok(responseDto);
        }
    }
}
