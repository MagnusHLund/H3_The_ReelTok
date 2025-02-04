using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateProfilePictureRequestDto")]
    public class GatewayUpdateProfilePictureRequestDto
    {
        [XmlElement("ProfilePicture")]
        public IFormFile ProfilePicture { get; set; }

        public GatewayUpdateProfilePictureRequestDto(IFormFile profilePicture)
        {
            ProfilePicture = profilePicture;
        }
    }
}