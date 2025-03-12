using System.Xml.Serialization;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs.GetUserById
{
    [XmlRoot("ReturnCreatedUser")]
    public class GetUserByIdResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public ExternalUserEntity User { get; set; }

        public GetUserByIdResponseDto(ExternalUserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
