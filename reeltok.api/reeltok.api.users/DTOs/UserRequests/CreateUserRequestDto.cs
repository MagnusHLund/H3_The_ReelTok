using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UserRequests
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