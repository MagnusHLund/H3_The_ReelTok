using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities
{
    public class CommentUsingUnixTime : BaseComment
    {
        public BaseCommentDetails<uint> CommentDetails { get; set; }

        public CommentUsingUnixTime(Guid commentId, BaseCommentDetails<uint> commentDetails) : base(commentId)
        {
            CommentDetails = commentDetails;
        }
    }
}