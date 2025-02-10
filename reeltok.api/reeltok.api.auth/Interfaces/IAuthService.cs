using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthService
    {
        Task RegisterUser(RegisterDetails registerDetails);
        Task<Tokens> LoginUser(LoginCredentials loginCredentials);
        Task<AccessToken> RefreshAccessToken(string refreshToken);
        Task DeleteUser(Guid userId);
        Task<Guid> GetUserIdByToken(string refreshToken);
        Task LogoutUser(string refreshToken);
    }
}
