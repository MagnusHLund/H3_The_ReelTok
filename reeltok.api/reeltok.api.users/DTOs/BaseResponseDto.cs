using System.ComponentModel.DataAnnotations;


namespace reeltok.api.users.DTOs
{

    internal abstract class BaseResponseDto
    {
        [Required]
        internal bool Success { get; private set; }

        private protected BaseResponseDto(bool success)
        {
            Success = success;
        }
    }
}