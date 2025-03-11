namespace reeltok.api.gateway.Interfaces.DTOs
{
    public interface ICommentUsingUnixTimeDto
    {
        Guid CommentId { get; set; }
        Guid UserId { get; set; }
        Guid VideoId { get; set; }
        string CommentText { get; set; }
        uint CreatedAt { get; set; }
    }
}
