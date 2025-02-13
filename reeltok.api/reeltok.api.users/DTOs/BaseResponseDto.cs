using System.ComponentModel.DataAnnotations;


namespace reeltok.api.users.DTOs
{

    public abstract class BaseResponseDto
    {
        [Required]
        public bool Success { get; private set; }

        private protected BaseResponseDto(bool success)
        {
            Success = success;
        }
    }
}