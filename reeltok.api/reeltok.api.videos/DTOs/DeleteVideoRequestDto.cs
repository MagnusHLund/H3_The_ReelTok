namespace reeltok.api.videos.DTOs
{
    internal class DeleteVideoRequestDto
    {
        internal Guid UserId { get; private set; }
        internal Guid VideoId { get; private set;}

        internal DeleteVideoRequestDto(Guid userId, Guid videoId){
            UserId = userId;
            VideoId = videoId;
        }
    }
}