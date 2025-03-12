using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.DTOs.Users.UpdateUserDetails
{
    public class ServiceUpdateUserDetailsRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [StringLength(25, MinimumLength = 3)]
        public string? Username { get; set; }

        [Range(1, 320)]
        [EmailAddress]
        public string? Email { get; set; }

        public CategoryType? Interest { get; set; }

        public ServiceUpdateUserDetailsRequestDto(Guid userId, string? username, string? email, CategoryType? interest)
        {
            UserId = userId;
            Username = username;
            Email = email;
            Interest = interest;
        }
    }
}
