using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("LoginRequestDto")]
    public class GatewayLoginRequestDto
    {
        [XmlElement("Email")]
        [Required]
        [Range(1, 320)]
        [EmailAddress]
        public string Email { get; set; }
        [XmlElement("Password")]
        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }

        public GatewayLoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
