using reeltok.api.gateway.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.GetUserProfileData
{
    public class ServiceGetUserProfileDataResponseDto : BaseResponseDto, IUserProfileDataDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string ProfileUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string ProfilePictureUrl { get; set; }

        public ServiceGetUserProfileDataResponseDto(Guid userId, string username, string profileUrl, string profilePictureUrl, string email = "", bool success = true) : base(success)
        {
            UserId = userId;
            Username = username;
            Email = email;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
