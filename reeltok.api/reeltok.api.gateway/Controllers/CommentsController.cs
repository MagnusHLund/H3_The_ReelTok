using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Comments;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Mappers;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddComment([FromBody] GatewayAddCommentRequestDto request)
        {
            if (string.IsNullOrEmpty(request.CommentText))
            {
                BadRequest(new FailureResponseDto("Comments must include text!"));
            }

            CommentUsingDateTime comment = await _commentsService.AddComment(request.VideoId, request.CommentText);

            GatewayAddCommentResponseDto responseDto = CommentMapper.ConvertToResponseDto(comment);

            return Ok(responseDto);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> LoadComments([FromBody] GatewayLoadCommentsRequestDto request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest(new FailureResponseDto("Amount should be greater than zero!"));
            }

            List<CommentUsingDateTime> comments = await _commentsService.LoadComments(request.VideoId, request.Amount);

            if (comments.Count.Equals(0))
            {
                return NoContent();
            }

            GatewayLoadCommentsResponseDto responseDto = new GatewayLoadCommentsResponseDto(comments);

            return Ok(responseDto);
        }
    }
}