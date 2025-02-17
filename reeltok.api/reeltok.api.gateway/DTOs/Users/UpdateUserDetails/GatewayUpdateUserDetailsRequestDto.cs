using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsRequestDto")]
    public class GatewayUpdateUserDetailsRequestDto
    {
        [XmlElement("Username")]
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }
        [XmlElement("Email")]
        [Required]
        [Range(1, 320)]
        [EmailAddress]
        public string Email { get; set; }

        public GatewayUpdateUserDetailsRequestDto(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
