using System.Xml.Serialization;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscriptionsForUserResponseDto")]
    public class ServiceGetAllSubscriptionsForUserResponseDto : BaseResponseDto
    {
        [XmlElement("Users")]
        [XmlArray("Users")]
        [XmlArrayItem("UserDetails")]
        public List<UserDetails> Users { get; set; }

        public ServiceGetAllSubscriptionsForUserResponseDto(List<UserDetails> users, bool success = true) : base(success)
        {
            Users = users;
        }

        public ServiceGetAllSubscriptionsForUserResponseDto() { }
    }
}