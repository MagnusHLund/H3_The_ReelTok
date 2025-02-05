using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("CreateUserRequestDto")]
    public class GatewayCreateUserRequestDto
    {
        [XmlElement(elementName: "Email")]
        public string Email { get; set; }
        [XmlElement(elementName: "Username")]
        public string Username { get; set; }
        [XmlElement(elementName: "Password")]
        public string Password { get; set; }

        public GatewayCreateUserRequestDto(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
}