using System.Xml.Serialization;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.CreateUser
{
    [XmlRoot("ReturnCreatedUser")]
    public class CreateUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserEntity User { get; set; }

        public CreateUserResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
