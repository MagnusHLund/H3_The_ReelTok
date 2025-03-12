using reeltok.api.gateway.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class GatewayUploadVideoResponseDto : BaseResponseDto
    {
        public GatewayUploadVideoResponseDto(bool success = true) : base(success) { }
    }
}
