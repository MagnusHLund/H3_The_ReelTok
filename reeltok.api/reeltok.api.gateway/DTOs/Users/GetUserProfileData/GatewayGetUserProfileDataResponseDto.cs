using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataResponseDto")]
    public class GatewayGetUserProfileDataResponseDto : BaseResponseDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("ProfileUrl")]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        public GatewayGetUserProfileDataResponseDto(Guid userId, string username, string profileUrl, string profilePictureUrl, bool success = true) : base(success)
        {
            UserId = userId;
            Username = username;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}