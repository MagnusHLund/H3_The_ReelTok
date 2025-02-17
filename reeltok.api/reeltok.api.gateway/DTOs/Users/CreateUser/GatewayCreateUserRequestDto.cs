using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("CreateUserRequestDto")]
    public class GatewayCreateUserRequestDto
    {
        [XmlElement(elementName: "Email")]
        [Required]
        [EmailAddress]
        [Range(1, 320)]

        public string Email { get; set; }
        [XmlElement(elementName: "Username")]
        [Required]
        [StringLength(25)]
        public string Username { get; set; }
        [XmlElement(elementName: "Password")]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public GatewayCreateUserRequestDto(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
