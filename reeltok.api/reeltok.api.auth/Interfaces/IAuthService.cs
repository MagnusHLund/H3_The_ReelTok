using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthService
    {
        Task<Tokens> CreateUser(CreateDetails CreateDetails);
        Task<Tokens> LoginUser(LoginCredentials loginCredentials);
        Task<AccessToken> RefreshAccessToken(string refreshToken);
        Task DeleteUser(Guid userId);
        Task<Guid> GetUserIdByToken(string refreshToken);
        Task LogoutUser(string refreshToken);
    }
}
