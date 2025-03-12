using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Users.CreateUser
{
    public class ServiceCreateUserRequestDto
    {
        [Required]
        [EmailAddress]
        [Range(1, 320)]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public byte Interests { get; set; }

        public ServiceCreateUserRequestDto(string email, string username, string password, byte interests)
        {
            Email = email;
            Username = username;
            Password = password;
            Interests = interests;
        }
    }
}
