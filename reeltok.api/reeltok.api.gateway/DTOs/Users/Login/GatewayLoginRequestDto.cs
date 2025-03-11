using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class GatewayLoginRequestDto
    {
        [Required]
        [Range(1, 320)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }

        public GatewayLoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
