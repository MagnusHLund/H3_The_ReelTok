using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Users.CreateUser
{
    public class GatewayCreateUserRequestDto
    {
        [Required]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public CategoryType Interest { get; set; }

        public GatewayCreateUserRequestDto(string email, string username, string password, CategoryType interest)
        {
            Email = email;
            Username = username;
            Password = password;
            Interest = interest;
        }
    }
}
