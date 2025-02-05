using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsRequestDto")]
    public class ServiceUpdateUserDetailsRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }

        public ServiceUpdateUserDetailsRequestDto(Guid userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}