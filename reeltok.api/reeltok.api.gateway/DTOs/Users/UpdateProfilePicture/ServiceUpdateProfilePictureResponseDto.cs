using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateProfilePictureResponseDto")]
    public class ServiceUpdateProfilePictureResponseDto : BaseResponseDto
    {
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        public ServiceUpdateProfilePictureResponseDto(string profilePictureUrl, bool success = true) : base(success)
        {
            ProfilePictureUrl = profilePictureUrl;
        }

        public ServiceUpdateProfilePictureResponseDto() { }
    }
}