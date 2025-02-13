using reeltok.api.auth.Entities;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthRepository
    {
        Task CreateUser(UserAuthentication userAuthentication);
        Task<RefreshToken> RefreshAccessToken(string refreshToken);
        Task DeleteUser(Guid userId);
        Task<Guid> GetUserIdByToken(string refreshToken);
        Task<UserAuthentication> GetUserAuthenticationByUserId(Guid userId);
        Task<bool> DoesUserExist(Guid userId);
        Task LogoutUser(string refreshToken);
    }
}
