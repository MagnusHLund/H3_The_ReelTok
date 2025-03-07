using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs
{
    public abstract class BaseResponseDto
    {
        [Required]
        public virtual bool Success { get; set; }

        private protected BaseResponseDto(bool success = true)
        {
            Success = success;
        }
    }
}