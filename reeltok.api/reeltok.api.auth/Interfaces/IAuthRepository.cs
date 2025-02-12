using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserCredentialsEntity> CreateUser(UserCredentialsEntity userCredentials);
        Task<RefreshToken> RefreshAccessToken(string refreshToken);
        Task DeleteUser(Guid userId);
        Task<UserCredentialsEntity> GetUserAuthenticationByUserId(Guid userId);
        Task<bool> DoesUserExist(Guid userId);
        Task LogoutUser(string refreshToken);
    }
}
