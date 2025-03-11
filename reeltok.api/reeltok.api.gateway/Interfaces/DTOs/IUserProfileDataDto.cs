namespace reeltok.api.gateway.Interfaces.DTOs
{
    public interface IUserProfileDataDto
    {
        Guid UserId { get; set; }
        string Email { get; set; }
        string Username { get; set; }
        string ProfileUrl { get; set; }
        string ProfilePictureUrl { get; set; }
    }
}
