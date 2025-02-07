using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateProfilePictureResponseDto")]
    public class GatewayUpdateProfilePictureResponseDto : BaseResponseDto
    {
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        public GatewayUpdateProfilePictureResponseDto(string profilePictureUrl, bool success = true) : base(success)
        {
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}