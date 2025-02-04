using System.Xml.Serialization;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscribingToUserResponseDto")]
    public class ServiceGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [XmlElement("Users")]
        public List<UserDetails> Users { get; set; }

        public ServiceGetAllSubscribingToUserResponseDto(List<UserDetails> users, bool success = true) : base(success)
        {
            Users = users;
        }
        public ServiceGetAllSubscribingToUserResponseDto() { }
    }
}