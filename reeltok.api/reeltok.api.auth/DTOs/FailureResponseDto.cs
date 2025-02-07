using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    internal class FailureResponseDto : BaseResponseDto
    {

        [Required]
        public string Message { get; set; }

        internal FailureResponseDto(string message, bool success) : base(success)
        {
          Message = message;
        }
    }
}
