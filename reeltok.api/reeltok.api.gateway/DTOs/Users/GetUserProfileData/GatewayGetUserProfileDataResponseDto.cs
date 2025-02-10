using System.ComponentModel.DataAnnotations;
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
        [StringLength(25)]
        public string Username { get; set; }
        [XmlElement("Email")]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }
        [XmlElement("ProfileUrl")]
        [StringLength(30)]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        [StringLength(50)]
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
