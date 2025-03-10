using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces.Services
{
    public interface IUsersService
    {
        Guid GetUserIdByAccessTokenAsync(string authenticationToken);
        Task<Tokens> SignUpAsync(Credentials userCredentials);
        Task DeleteUser(Guid userId);
    }
}