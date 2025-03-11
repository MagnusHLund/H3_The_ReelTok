using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.LoadComments
{
    public class GatewayLoadCommentsRequestDto
    {
        [Required]
        public Guid VideoId { get; set; }

        [Required]
        public byte Amount { get; set; }

        public GatewayLoadCommentsRequestDto(Guid videoId, byte amount)
        {
            VideoId = videoId;
            Amount = amount;
        }
    }
}
