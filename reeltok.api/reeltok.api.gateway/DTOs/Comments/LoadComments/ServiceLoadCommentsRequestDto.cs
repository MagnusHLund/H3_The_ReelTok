using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Comments.LoadComments
{
    public class ServiceLoadCommentsRequestDto
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("PageNumber")]
        public int PageNumber { get; set; }

        [Required]
        [JsonProperty("PageSize")]
        public byte PageSize { get; set; }

        public ServiceLoadCommentsRequestDto(Guid videoId, int pageNumber, byte pageSize)
        {
            VideoId = videoId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
