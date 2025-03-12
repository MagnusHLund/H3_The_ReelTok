namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    public class ServiceGetVideosForProfileRequestDto
    {
        public Guid UserId { get; set; }
        public uint PageNumber { get; set; }
        public byte PageSize { get; set; }

        public ServiceGetVideosForProfileRequestDto(Guid userId, uint pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
