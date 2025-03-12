using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.CreateUser
{
    public class ServiceCreateUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("User")]
        public UserEntity User { get; set; }

        public ServiceCreateUserResponseDto(UserEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
