namespace reeltok.api.gateway.ValueObjects
{
    public abstract class BaseCommentDetails<T>
    {
        public Guid UserId { get; }
        public Guid VideoId { get; }
        public string Message { get; }
        public abstract T CreatedAt { get; }

        protected BaseCommentDetails(Guid userId, Guid videoId, string message)
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
        }
    }
}
