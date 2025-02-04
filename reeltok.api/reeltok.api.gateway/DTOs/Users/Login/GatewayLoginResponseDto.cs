using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("LoginResponseDto")]
    public class GatewayLoginResponseDto : BaseResponseDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("ProfileUrl")]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }
        public GatewayLoginResponseDto(Guid userId, string email, string username, string profileUrl, string profilePictureUrl, bool success = true) : base(success)
        {
            UserId = userId;
            Email = email;
            Username = username;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}