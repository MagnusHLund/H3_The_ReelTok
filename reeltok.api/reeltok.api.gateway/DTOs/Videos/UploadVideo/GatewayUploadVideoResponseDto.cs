using reeltok.api.gateway.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class GatewayUploadVideoResponseDto : BaseResponseDto
    {
        [Required]
        public Video Video { get; set; }

        public GatewayUploadVideoResponseDto(Video video)
        {
            Video = video;
        }
    }
}
