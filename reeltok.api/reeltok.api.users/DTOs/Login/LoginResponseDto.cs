using Newtonsoft.Json;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.Login
{
    public class LoginResponseDto : BaseResponseDto
    {
        private UserWithInterestEntity user;

        [Required]
        [JsonProperty("User")]
        public UserWithInterestEntity User { get; set; }


        public LoginResponseDto(UserWithInterestEntity user, bool success = true) : base(success)
        {
            User = user;
        }
    }
}
