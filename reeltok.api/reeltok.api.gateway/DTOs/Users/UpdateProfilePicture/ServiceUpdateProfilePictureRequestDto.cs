using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateProfilePictureRequestDto")]
    public class ServiceUpdateProfilePictureRequestDto
    {
        [XmlElement("UserId")]
        [Required]
        public Guid UserId { get; set; }
        [XmlElement("ProfilePicture")]
        [Required]

        public IFormFile ProfilePicture { get; set; }

        public ServiceUpdateProfilePictureRequestDto(Guid userId, IFormFile profilePicture)
        {
            UserId = userId;
            ProfilePicture = profilePicture;
        }
    }
}
