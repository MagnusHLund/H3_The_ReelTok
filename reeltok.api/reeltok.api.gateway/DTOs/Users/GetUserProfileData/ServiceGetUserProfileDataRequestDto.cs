using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataRequestDto")]
    public class ServiceGetUserProfileDataRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }

        public ServiceGetUserProfileDataRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}