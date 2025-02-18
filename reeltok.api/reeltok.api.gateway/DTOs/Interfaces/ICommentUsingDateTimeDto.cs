namespace reeltok.api.gateway.DTOs.Interfaces
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
