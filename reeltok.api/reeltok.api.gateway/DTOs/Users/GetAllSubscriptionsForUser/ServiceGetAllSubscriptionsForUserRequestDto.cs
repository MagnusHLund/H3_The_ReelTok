using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser
{
    public class ServiceGetAllSubscriptionsForUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public uint PageNumber {get; set;}

        [Required]
        public byte PageSize {get; set;}

        public ServiceGetAllSubscriptionsForUserRequestDto(Guid userId, uint pageNumber, byte pageSize)
        {
            UserId = userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
