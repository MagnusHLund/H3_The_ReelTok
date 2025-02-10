using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UserRequestDTO
{
    public class CreateUserRequestDto
    {
        [Required]
        public string UserName { get; }
        [Required]
        public string ProfilePictureUrl { get; }
        [Required]
        public string ProfileUrl { get; }
        [Required]
        public string Email { get; }



    public CreateUserRequestDto(string userName, string profilePictureUrl, string profileUrl, string email)
        {
            UserName = userName;
            ProfilePictureUrl = profilePictureUrl;
            ProfileUrl = profileUrl;
            Email = email;
        }
    }
}