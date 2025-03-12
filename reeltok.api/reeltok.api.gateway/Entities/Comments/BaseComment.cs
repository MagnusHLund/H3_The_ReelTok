namespace reeltok.api.gateway.Entities.comments
{
    public abstract class BaseComment
    {
        public uint CommentId { get; set; }

        protected BaseComment(uint commentId)
        {
            CommentId = commentId;
        }
    }
}
