using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsResponseDto")]
    public class GatewayUpdateUserDetailsResponseDto : BaseResponseDto
    {
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }
        public GatewayUpdateUserDetailsResponseDto(string username, string email, bool success = true) : base(success)
        {
            Username = username;
            Email = email;
        }

        public GatewayUpdateUserDetailsResponseDto(bool success = true) : base(success) { }
    }
}