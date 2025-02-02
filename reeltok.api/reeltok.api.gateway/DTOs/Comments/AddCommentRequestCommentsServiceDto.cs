using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs.Comments
{
    public class AddCommentRequestCommentsServiceDto
    {
        public Guid VideoId { get; set; }
        public string CommentText { get; set; }

        public AddCommentRequestCommentsServiceDto(Guid videoId, string commentText)
        {
            VideoId = videoId;
            CommentText = commentText;
        }
    }
}