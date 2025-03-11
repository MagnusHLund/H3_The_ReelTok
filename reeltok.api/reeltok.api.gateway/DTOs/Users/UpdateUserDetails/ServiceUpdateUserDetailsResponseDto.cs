using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class ServiceUpdateUserDetailsResponseDto : BaseResponseDto
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }

        public ServiceUpdateUserDetailsResponseDto(string username, string email, bool success = true) : base(success)
        {
            Username = username;
            Email = email;
        }
    }
}
