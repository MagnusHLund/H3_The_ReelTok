using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataResponseDto")]
    public class ServiceGetUserProfileDataResponseDto : BaseResponseDto
    {
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
        [XmlElement("ProfileUrl")]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        public ServiceGetUserProfileDataResponseDto(string username, string profileUrl, string profilePictureUrl, string email = "", bool success = true) : base(success)
        {
            Username = username;
            Email = email;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }

        public ServiceGetUserProfileDataResponseDto() { }
    }
}