using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("CreateUserRequestDto")]
    public class GatewayCreateUserRequestDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public GatewayCreateUserRequestDto(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
}