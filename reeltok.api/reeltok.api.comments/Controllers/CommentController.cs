using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost("Create a comment")]
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

        

    }
}
