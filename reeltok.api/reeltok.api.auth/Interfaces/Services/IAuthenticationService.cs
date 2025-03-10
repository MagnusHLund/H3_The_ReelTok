using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<Tokens> LoginUserAsync(Credentials loginCredentials);
        Task LogoutUserAsync(string accessToken, string refreshToken);
    }
}
