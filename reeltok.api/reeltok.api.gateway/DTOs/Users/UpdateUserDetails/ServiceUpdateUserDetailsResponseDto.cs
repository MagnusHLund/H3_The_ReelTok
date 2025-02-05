using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsResponseDto")]
    public class ServiceUpdateUserDetailsResponseDto : BaseResponseDto
    {
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        public ServiceUpdateUserDetailsResponseDto(string username, string email, bool success = true) : base(success)
        {
            Username = username;
            Email = email;
        }
        public ServiceUpdateUserDetailsResponseDto() { }
    }
}