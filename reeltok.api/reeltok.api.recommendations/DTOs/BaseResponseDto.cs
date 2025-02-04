
using System.ComponentModel.DataAnnotations;


namespace reeltok.api.recommendations.DTOs
{
    public abstract class BaseResponseDto
    {
        [Required]
        public bool Success { get; set; }

        private protected BaseResponseDto(bool success)
        {
            Success = success;
        }
    }
}