using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    public class ServiceGetVideosForProfileRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("PageNumber")]
        public int PageNumber { get; set; }

        [Required]
        [JsonProperty("PageSize")]
        public byte PageSize { get; set; }

        public ServiceGetVideosForProfileRequestDto(Guid userId, int pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
