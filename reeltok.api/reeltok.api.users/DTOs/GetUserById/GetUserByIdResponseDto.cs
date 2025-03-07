using System.Xml.Serialization;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.GetUserById
{
    [XmlRoot("ReturnCreatedUser")]
    public class GetUserByIdResponseDto : BaseResponseDto
    {
        [Required]
        public ExternalUserEntity User { get; set; }

        public GetUserByIdResponseDto(ExternalUserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
