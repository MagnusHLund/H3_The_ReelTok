using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.DTOs
{
    public abstract class BaseResponseDto
    {
        [Required]
        [JsonProperty("Success")]
        public virtual bool Success { get; set; }

        private protected BaseResponseDto(bool success = true)
        {
            Success = success;
        }
    }
}