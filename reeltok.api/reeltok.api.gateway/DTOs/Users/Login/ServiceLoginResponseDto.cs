using reeltok.api.gateway.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.Login
{
    public class ServiceLoginResponseDto : BaseResponseDto, IUserProfileDataDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(30)]
        public string ProfileUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string ProfilePictureUrl { get; set; }

        public ServiceLoginResponseDto(Guid userId, string email, string username, string profileUrl, string profilePictureUrl, bool success = true) : base(success)
        {
            UserId = userId;
            Email = email;
            Username = username;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
