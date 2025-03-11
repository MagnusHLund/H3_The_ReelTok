namespace reeltok.api.gateway.Interfaces.DTOs
{
    public interface ICommentUsingDateTimeDto
    {
        Guid CommentId { get; set; }
        Guid UserId { get; set; }
        Guid VideoId { get; set; }
        string CommentText { get; set; }
        DateTime CreatedAt { get; set; }
    }
}
