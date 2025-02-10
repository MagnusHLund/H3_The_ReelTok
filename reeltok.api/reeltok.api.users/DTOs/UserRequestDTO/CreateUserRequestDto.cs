using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UserRequestDTO
{
    public class CreateUserRequestDto
    {
        [Required]
        public string UserName { get; }
        [Required]
        public string Email { get; }



    public CreateUserRequestDto(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}