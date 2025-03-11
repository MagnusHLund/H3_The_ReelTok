using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class ServiceUpdateUserDetailsRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [Range(1, 320)]
        [EmailAddress]
        public string Email { get; set; }

        public ServiceUpdateUserDetailsRequestDto(Guid userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}
