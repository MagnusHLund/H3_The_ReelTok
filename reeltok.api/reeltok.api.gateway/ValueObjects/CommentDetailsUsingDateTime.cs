namespace reeltok.api.gateway.ValueObjects
{
    public class CommentDetailsUsingDateTime : BaseCommentDetails<DateTime>
    {
        public override DateTime CreatedAt { get; }

        public CommentDetailsUsingDateTime(
            Guid userId,
            Guid videoId,
            string message,
            DateTime createdAt
        ) : base(userId, videoId, message)
        {
            CreatedAt = createdAt;
        }
    }
}
