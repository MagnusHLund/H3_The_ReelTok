namespace reeltok.api.gateway.Entities
{
    public abstract class BaseComment
    {
        public Guid CommentId { get; set; }

        protected BaseComment(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}
