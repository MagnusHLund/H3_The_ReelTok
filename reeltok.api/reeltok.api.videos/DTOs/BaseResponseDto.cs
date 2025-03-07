using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs
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
    }
}
