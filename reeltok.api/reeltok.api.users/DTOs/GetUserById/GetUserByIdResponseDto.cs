using System.Xml.Serialization;
using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.GetUserById
{
    [XmlRoot("ReturnCreatedUser")]
    public class GetUserByIdResponseDto : BaseResponseDto
    {
        public ExternalUserEntity User { get; set; }

        public GetUserByIdResponseDto(ExternalUserEntity user, bool success = true) : base(success)
        {
            User = user;
        }

        public GetUserByIdResponseDto() { }
    }
}
