using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class ServiceGetAllSubscribingToUserRequestDto
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

        public ServiceGetAllSubscribingToUserRequestDto(Guid userId, int pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
