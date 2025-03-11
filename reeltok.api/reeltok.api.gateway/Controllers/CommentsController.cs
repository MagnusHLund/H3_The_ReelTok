using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ActionFilters;
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

            CommentUsingDateTime comment = await _commentsService.AddComment(request.VideoId, request.CommentText).ConfigureAwait(false);

            GatewayAddCommentResponseDto responseDto = CommentMapper.ConvertToResponseDto<GatewayAddCommentResponseDto>(comment);

            return Ok(responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> LoadCommentsAsync([FromBody] GatewayLoadCommentsRequestDto request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest(new FailureResponseDto("Amount should be greater than zero!"));
            }

            List<CommentUsingDateTime> comments = await _commentsService.LoadComments(request.VideoId, request.Amount).ConfigureAwait(false);

            if (comments.Count.Equals(0))
            {
                return NoContent();
            }

            GatewayLoadCommentsResponseDto responseDto = new GatewayLoadCommentsResponseDto(comments);

            return Ok(responseDto);
        }
    }
}
