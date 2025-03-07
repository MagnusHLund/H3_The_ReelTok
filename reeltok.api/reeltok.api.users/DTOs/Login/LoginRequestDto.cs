namespace reeltok.api.users.DTOs.Login
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public LoginRequestDto() { }
    }
}