using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class ServiceLoginRequestDto
    {
        [Required]
        [Range(1, 320)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }

        public ServiceLoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
