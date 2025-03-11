using reeltok.api.gateway.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class ServiceUploadVideoResponseDto : BaseResponseDto
    {
        [Required]
        public Video Video { get; set; }

        public ServiceUploadVideoResponseDto(Video video, bool success = true) : base(success)
        {
            Video = video;
        }
    }
}
