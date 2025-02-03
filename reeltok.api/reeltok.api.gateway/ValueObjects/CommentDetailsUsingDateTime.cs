namespace reeltok.api.gateway.ValueObjects
{
    public class CommentDetailsUsingDateTime : BaseCommentDetails<DateTime>
    {
        public override DateTime CreatedAt { get; }

        public CommentDetailsUsingDateTime(Guid userId, Guid videoId, string commentText, DateTime createdAt) : base(userId, videoId, commentText)
        {
            CreatedAt = createdAt;
        }
    }
}