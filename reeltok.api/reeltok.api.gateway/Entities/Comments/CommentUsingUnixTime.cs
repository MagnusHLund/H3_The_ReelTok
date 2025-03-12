using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.comments
{
    public class CommentUsingUnixTime : BaseComment
    {
        public BaseCommentDetails<uint> CommentDetails { get; set; }

        public CommentUsingUnixTime(uint commentId, BaseCommentDetails<uint> commentDetails) : base(commentId)
        {
            CommentDetails = commentDetails;
        }
    }
}
