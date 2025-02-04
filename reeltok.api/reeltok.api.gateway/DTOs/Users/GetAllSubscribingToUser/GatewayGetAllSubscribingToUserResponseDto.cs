using System.Xml.Serialization;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscribingToUserResponseDto")]
    public class GatewayGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [XmlElement("Users")]
        public List<UserDetails> Users { get; set; }

        public GatewayGetAllSubscribingToUserResponseDto(List<UserDetails> users, bool success) : base(success)
        {
            Users = users;
        }

        public GatewayGetAllSubscribingToUserResponseDto() { }
    }
}