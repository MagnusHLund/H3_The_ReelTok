using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.comments
{
    public class CommentUsingDateTime : BaseComment
    {
        public BaseCommentDetails<DateTime> CommentDetails { get; set; }

        public CommentUsingDateTime(uint commentId, BaseCommentDetails<DateTime> commentDetails) : base(commentId)
        {
            CommentId = commentId;
            CommentDetails = commentDetails;
        }
    }
}
