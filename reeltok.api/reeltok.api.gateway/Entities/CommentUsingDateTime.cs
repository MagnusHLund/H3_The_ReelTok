using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities
{
    public class CommentUsingDateTime : BaseComment
    {
        public BaseCommentDetails<DateTime> CommentDetails { get; set; }

        public CommentUsingDateTime(Guid commentId, BaseCommentDetails<DateTime> commentDetails) : base(commentId)
        {
            CommentId = commentId;
            CommentDetails = commentDetails;
        }
    }
}
