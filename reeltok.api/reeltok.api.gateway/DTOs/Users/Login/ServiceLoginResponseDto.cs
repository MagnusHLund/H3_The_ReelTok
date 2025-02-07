using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("LoginResponseDto")]
    public class ServiceLoginResponseDto : BaseResponseDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Email")]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }
        [XmlElement("Username")]
        [StringLength(25, MinimumLength = 3)]

        public string Username { get; set; }
        [XmlElement("ProfileUrl")]
        [StringLength(30)]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        [StringLength(50)]
        public string ProfilePictureUrl { get; set; }
        public ServiceLoginResponseDto(Guid userId, string email, string username, string profileUrl, string profilePictureUrl, bool success = true) : base(success)
        {
            UserId = userId;
            Email = email;
            Username = username;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }

        public ServiceLoginResponseDto() { }
    }
}