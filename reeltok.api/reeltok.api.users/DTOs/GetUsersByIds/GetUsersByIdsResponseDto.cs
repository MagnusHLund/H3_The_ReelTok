using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.GetUsersByIds
{
    public class GetUsersByIdsResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Users")]
        public List<ExternalUserEntity> Users { get; set; }

        public GetUsersByIdsResponseDto(List<ExternalUserEntity> users, bool success = true) : base(success)
        {
            Users = users;
        }
    }
}