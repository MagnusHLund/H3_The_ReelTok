using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs.Comments
{
    public class AddCommentRequestDto
    {
        public Guid VideoId { get; set; }
        public string CommentText { get; set; }

        public AddCommentRequestDto(Guid videoId, string commentText)
        {
            VideoId = videoId;
            CommentText = commentText;
        }
    }
}