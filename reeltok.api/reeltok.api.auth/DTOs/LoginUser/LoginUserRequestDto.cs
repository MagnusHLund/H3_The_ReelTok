using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.auth.DTOs
{
    [XmlRoot("CreateUserRequestDto")]
    public class LoginUserRequestDto
    {
        [Required]
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [Required]
        [XmlElement("Password")]
        public string PlainTextPassword { get; set; }

        public LoginUserRequestDto(Guid userId, string plainTextPassword)
        {
            UserId = userId;
            PlainTextPassword = plainTextPassword;
        }

        public LoginUserRequestDto() { }
    }
}
