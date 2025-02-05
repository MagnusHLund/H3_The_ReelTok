using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsRequestDto")]
    public class GatewayUpdateUserDetailsRequestDto
    {
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }

        public GatewayUpdateUserDetailsRequestDto(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}