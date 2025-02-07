using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateProfilePictureRequestDto")]
    public class GatewayUpdateProfilePictureRequestDto
    {
        [XmlElement("ProfilePicture")]
        [Required]
        public IFormFile ProfilePicture { get; set; }

        public GatewayUpdateProfilePictureRequestDto(IFormFile profilePicture)
        {
            ProfilePicture = profilePicture;
        }
    }
}