using System.Xml.Serialization;
using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.CreateUser
{
    [XmlRoot("ReturnCreatedUser")]
    public class CreateUserResponseDto : BaseResponseDto
    {
        public UserEntity User { get; set; }

        public CreateUserResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }

        public CreateUserResponseDto() { }
    }
}
