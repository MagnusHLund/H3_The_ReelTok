using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace reeltok.api.users.DTOs.UserResponses
{
    [XmlRoot("ReturnCreatedUser")]
    public class ReturnCreateUserResponseDTO
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Username")]
        public string UserName { get; set; }
        [XmlElement("ProfileUrl")]
        public string ProfileUrl { get; set; }
        [XmlElement("ProfilePictureUrl")]
        public string ProfilePictureUrl { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }

        public ReturnCreateUserResponseDTO( Guid userId, string email, string userName, string profileUrl, string profilePictureUrl)
        {
            UserId = userId;
            Email = email;
            UserName = userName;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }

        public ReturnCreateUserResponseDTO()
        {

        }


    }
}
