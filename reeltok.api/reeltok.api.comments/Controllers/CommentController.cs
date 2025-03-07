using Microsoft.AspNetCore.Mvc;
using reeltok.api.comments.DTOs;
using reeltok.api.comments.Entities;
using reeltok.api.comments.Interface;
using reeltok.api.comments.Mappers;
using reeltok.api.comments.ValueObjects;

namespace reeltok.api.comments.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        public CommentController(ICommentService commentService)
        {
            _service = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (dto == null)
            {
                return BadRequest("Comment cannot be null");
            }

            CommentDetails commentDetails = dto.ToCommentFromCreateDTO();

            Comment comment = new Comment(commentDetails);

            Comment dbComment = await _service.CreateCommentAsync(comment).ConfigureAwait(false);

            ReturnCreateDTO returnCreateDTO = CommentMapper.ToReturnCreateCommentResponseDTO(dbComment);

            return Ok(returnCreateDTO);

        }

        // TODO: Call Video API to check if the video id is valid or not
        [HttpGet("by-video")]
        public async Task<IActionResult> GetAllCommentsByVideoIdAsync([FromQuery] Guid videoId)
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
                List<Comment> comments = await _service.GetAllCommentByVideoId(videoId).ConfigureAwait(false);
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
