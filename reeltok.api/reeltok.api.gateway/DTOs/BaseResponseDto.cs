using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs
{
    public abstract class BaseResponseDto
    {
        [Required]
        public virtual bool Success { get; set; } = true;

        protected BaseResponseDto(bool success)
        {
            Success = success;
        }
        protected BaseResponseDto() { }
    }
}
