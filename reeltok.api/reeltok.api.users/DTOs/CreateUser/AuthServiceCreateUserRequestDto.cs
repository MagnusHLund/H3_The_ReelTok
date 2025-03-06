namespace reeltok.api.users.DTOs.CreateUser
{
    public class AuthServiceCreateUserRequestDto
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }

        public AuthServiceCreateUserRequestDto(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public AuthServiceCreateUserRequestDto() { }
    }
}