using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Entites;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthRepository
    {
      public Task RegisterUser(Auth authInfo);
      public Task<Tokens> LoginUser(LoginCredentials loginCredentials);
      public Task<AccessToken> RefreshAccessToken(string refreshToken);
      public Task DeleteUser(Guid userId);
      public Task<Guid> GetUserIdByToken(string refreshToken);
      public Task<Auth?> GetAuthByUserId(Guid userId);
      public Task LogoutUser(string refreshToken);
    }
}
