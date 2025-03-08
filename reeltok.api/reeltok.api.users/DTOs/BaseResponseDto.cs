using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs
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