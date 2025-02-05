using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("LoginRequestDto")]
    public class GatewayLoginRequestDto
    {
        [XmlElement("Email")]
        public string Email { get; set; }
        [XmlElement("Password")]
        public string Password { get; set; }

        public GatewayLoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}