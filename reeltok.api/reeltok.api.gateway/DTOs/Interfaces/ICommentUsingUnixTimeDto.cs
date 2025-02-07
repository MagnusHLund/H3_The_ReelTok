namespace reeltok.api.gateway.DTOs.Interfaces
{
    public interface ICommentUsingUnixTimeDto
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public string CommentText { get; set; }
        public uint CreatedAt { get; set; }
    }
}