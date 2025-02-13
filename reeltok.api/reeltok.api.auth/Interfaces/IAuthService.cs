using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthService
    {
        Task<Tokens> CreateUser(CreateDetails CreateDetails);
        Task<Tokens> LoginUser(LoginCredentials loginCredentials);
        Task DeleteUser(Guid userId);
        Guid GetUserIdByToken(string accessTokenValue);
        Task LogoutUser(string accessToken, string refreshToken);
    }
}
