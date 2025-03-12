namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    public class ServiceGetVideosForProfileRequestDto
    {
        public Guid UserId { get; set; }
        public int PageNumber { get; set; }
        public byte PageSize { get; set; }

        public ServiceGetVideosForProfileRequestDto(Guid userId, int pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
