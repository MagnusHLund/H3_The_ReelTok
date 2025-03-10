using Microsoft.AspNetCore.Mvc;
using reeltok.api.comments.Mappers;
using reeltok.api.comments.Entities;
using reeltok.api.comments.Interface;
using reeltok.api.comments.ValueObjects;
using reeltok.api.comments.ActionFilters;
using reeltok.api.comments.DTOs.CreateComment;

namespace reeltok.api.comments.Controllers
{
    [ValidateModel]
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        public CommentController(ICommentService commentService)
        {
            _service = commentService;
        }

        // TODO: Call video api to ensure that the video exists
        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentRequestDto request)
        {

            CommentDetails commentDetails = dto.ToCommentFromCreateDTO();

            CommentEntity comment = new CommentEntity(commentDetails);

            CommentEntity dbComment = await _service.CreateCommentAsync(comment).ConfigureAwait(false);

            ReturnCreateDTO returnCreateDTO = CommentMapper.ToReturnCreateCommentResponseDTO(dbComment);

            return Ok(returnCreateDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommentsByVideoIdAsync(
            [FromQuery] Guid videoId,
            [FromQuery] uint pageNumber,
            [FromQuery] byte pageSize
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (videoId == Guid.Empty) // Check for empty GUID
            {
                return BadRequest("Request cannot be null or empty");
            }

            // TODO: you will call the Video API to verify the GUID

            try
            {
                List<CommentEntity> comments = await _service.GetAllCommentByVideoId(videoId).ConfigureAwait(false);
                List<ReadDTO> mappedComments = comments.Select(comment => comment.ToDTOFromCommentEntity()).ToList();

                return Ok(mappedComments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
