using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser
{
    public class ServiceGetAllSubscribingToUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public uint PageNumber { get; set; }

        [Required]
        public byte PageSize { get; set; }

        public ServiceGetAllSubscribingToUserRequestDto(Guid userId, uint pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
