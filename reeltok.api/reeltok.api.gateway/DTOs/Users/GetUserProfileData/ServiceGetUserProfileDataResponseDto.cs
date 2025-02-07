using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.DTOs.Interfaces;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataResponseDto")]
    public class ServiceGetUserProfileDataResponseDto : BaseResponseDto, IUserProfileDataDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Username")]
        [StringLength(50, MinimumLength = 3)] 

        public string Username { get; set; }
        [XmlElement("email")]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }
        [XmlElement("ProfileUrl")]
        [StringLength(30)]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        [StringLength(50)]
        public string ProfilePictureUrl { get; set; }

        public ServiceGetUserProfileDataResponseDto(Guid userId, string username, string profileUrl, string profilePictureUrl, string email = "", bool success = true) : base(success)
        {
            UserId = userId;
            Username = username;
            Email = email;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }

        public ServiceGetUserProfileDataResponseDto() { }
    }
}