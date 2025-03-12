namespace reeltok.api.gateway.ValueObjects
{
    public class CommentDetailsUsingUnixTime : BaseCommentDetails<uint>
    {
        public override uint CreatedAt { get; }

        public CommentDetailsUsingUnixTime(
            Guid userId,
            Guid videoId,
            string message,
            uint createdAt
        ) : base(userId, videoId, message)
        {
            CreatedAt = createdAt;
        }
    }
}
