using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataResponseDto")]
    public class GatewayGetUserProfileDataResponseDto : BaseResponseDto
    {
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("ProfileUrl")]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        public GatewayGetUserProfileDataResponseDto(string username, string profileUrl, string profilePictureUrl, string email = "", bool success = true) : base(success)
        {
            Username = username;
            Email = email;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}