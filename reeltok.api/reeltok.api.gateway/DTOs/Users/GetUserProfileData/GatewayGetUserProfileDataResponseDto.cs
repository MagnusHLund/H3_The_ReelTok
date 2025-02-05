using System.Xml.Serialization;
using reeltok.api.gateway.DTOs.Interfaces;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataResponseDto")]
    public class GatewayGetUserProfileDataResponseDto : BaseResponseDto, IUserProfileDataDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("ProfileUrl")]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }

        public GatewayGetUserProfileDataResponseDto(Guid userId, string username, string profileUrl, string profilePictureUrl, string email = "", bool success = true) : base(success)
        {
            UserId = userId;
            Username = username;
            Email = email;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }

        public GatewayGetUserProfileDataResponseDto() { }
    }
}