using Newtonsoft.Json;
using reeltok.api.users.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UpdateUser
{
    public class UpdateUserResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("UserEntity")]
        public UserEntity UserEntity { get; set; }

        public UpdateUserResponseDto(UserEntity userEntity, bool success = true) : base(success)
        {
            UserEntity = userEntity;
        }
    }
}
