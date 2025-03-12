using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser
{
    public class ServiceGetAllSubscriptionsForUserRequestDto
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

        public ServiceGetAllSubscriptionsForUserRequestDto(Guid userId, int pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
