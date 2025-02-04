using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataResponseDto")]
    public class ServiceGetUserProfileDataResponseDto : BaseResponseDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }
        [XmlElement("ProfileUrl")]
        public string ProfileUrl { get; set; }

        public ServiceGetUserProfileDataResponseDto(Guid userId, string username, string profilePictureUrl, string profileUrl, bool success) : base(success)
        {
            UserId = userId;
            Username = username;
            ProfilePictureUrl = profilePictureUrl;
            ProfileUrl = profileUrl;
        }

        public ServiceGetUserProfileDataResponseDto() { }
    }
}