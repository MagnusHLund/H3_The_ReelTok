using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    public class ServiceGetVideosForFeedRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public byte Amount { get; set; }

        public ServiceGetVideosForFeedRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}
