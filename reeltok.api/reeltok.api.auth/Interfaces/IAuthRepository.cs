using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Entites;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthRepository
    {
        Task RegisterUser(Auth authInfo);
        Task<RefreshToken> RefreshAccessToken(string refreshToken);
        Task DeleteUser(Guid userId);
        Task<Guid> GetUserIdByToken(string refreshToken);
        Task<Auth?> GetAuthByUserId(Guid userId);
        Task LogoutUser(string refreshToken);
    }
}
