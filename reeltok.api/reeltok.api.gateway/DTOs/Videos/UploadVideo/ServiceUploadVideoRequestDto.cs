using reeltok.api.gateway.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class ServiceUploadVideoRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public VideoUpload Video { get; set; }

        public ServiceUploadVideoRequestDto(Guid userId, VideoUpload video)
        {
            UserId = userId;
            Video = video;
        }
    }
}
