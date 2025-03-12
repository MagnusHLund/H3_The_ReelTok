using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class ServiceGetAllSubscribingToUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public byte PageSize { get; set; }

        public ServiceGetAllSubscribingToUserRequestDto(Guid userId, int pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
