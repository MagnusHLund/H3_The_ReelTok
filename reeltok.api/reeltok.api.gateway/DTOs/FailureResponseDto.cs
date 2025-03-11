using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs
{
    public class FailureResponseDto : BaseResponseDto
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public override bool Success { get; set; }

        public FailureResponseDto(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
