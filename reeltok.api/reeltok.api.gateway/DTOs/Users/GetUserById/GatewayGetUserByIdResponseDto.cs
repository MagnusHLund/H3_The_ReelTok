using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Interfaces.DTOs;

namespace reeltok.api.gateway.DTOs.Users.GetUserProfileData
{
    public class GatewayGetUserByIdResponseDto : BaseResponseDto, IUserProfileDataDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(25)]
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

        public GatewayGetUserByIdResponseDto(
            Guid userId,
            string username,
            string profileUrl,
            string profilePictureUrl,
            string email,
            bool success = true
        ) : base(success)
        {
            UserId = userId;
            Username = username;
            Email = email;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }

        public GatewayGetUserByIdResponseDto() { }
    }
}
