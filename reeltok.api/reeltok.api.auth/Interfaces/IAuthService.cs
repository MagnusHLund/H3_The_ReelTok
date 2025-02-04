using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Entites;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface IAuthService
    {
      public void RegisterUser(Guid userId, string plainTextPassword);
      public Tokens LoginUser(LoginCredentials loginCredentials);
      public AccessToken RefreshAccessToken(string refreshToken);
      public void DeleteUser(Guid userId);
      public Guid GetUserIdByToken(string refreshToken);
      public void LogoutUser(string refreshToken);
      public void ChangeUserPassword(Guid userId, string newPassword);
    }
}
