using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UserRequestDTO
{
    internal class CreateUserRequestDto
    {
        [Required]
        internal string UserName { get; }
        [Required]
        internal string ProfilePictureUrl { get; }
        [Required]
        internal string ProfileUrl { get; }
        [Required]
        internal string Email { get; }



    internal CreateUserRequestDto(string userName, string profilePictureUrl, string profileUrl, string email)
        {
            UserName = userName;
            ProfilePictureUrl = profilePictureUrl;
            ProfileUrl = profileUrl;
            Email = email;
        }
    }
}