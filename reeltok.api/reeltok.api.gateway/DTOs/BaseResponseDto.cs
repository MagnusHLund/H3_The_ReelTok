using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs
{
    public abstract class BaseResponseDto
    {
        [Required]
        [JsonProperty("Success")]
        public virtual bool Success { get; set; } = true;

        protected BaseResponseDto(bool success)
        {
            Success = success;
        }
        protected BaseResponseDto() { }
    }
}
