using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.DTOs.Interfaces;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsResponseDto")]
    public class GatewayUpdateUserDetailsResponseDto : BaseResponseDto, IEditableUserDetailsDto
    {
        [XmlElement("Username")]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }
        [XmlElement("Email")]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }
        public GatewayUpdateUserDetailsResponseDto(string username, string email, bool success = true) : base(success)
        {
            Username = username;
            Email = email;
        }

        public GatewayUpdateUserDetailsResponseDto() { }
    }
}