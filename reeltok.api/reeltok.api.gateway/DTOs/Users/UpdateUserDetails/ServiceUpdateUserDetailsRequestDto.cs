using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsRequestDto")]
    public class ServiceUpdateUserDetailsRequestDto
    {
        [XmlElement("UserId")]
        [Required]
        public Guid UserId { get; set; }
        [XmlElement("Username")]
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }
        [XmlElement("Email")]
        [Required]
        [Range(1, 320)]
        [EmailAddress]
        public string Email { get; set; }

        public ServiceUpdateUserDetailsRequestDto(Guid userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}
