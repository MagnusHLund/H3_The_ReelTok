using System.Xml.Serialization;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscriptionsForUserResponseDto")]
    public class GatewayGetAllSubscriptionsForUserResponseDto : BaseResponseDto
    {
        [XmlElement("Users")]
        public List<UserDetails> Users { get; set; }

        public GatewayGetAllSubscriptionsForUserResponseDto(List<UserDetails> users, bool success) : base(success)
        {
            Users = users;
        }

        public GatewayGetAllSubscriptionsForUserResponseDto() { }
    }
}