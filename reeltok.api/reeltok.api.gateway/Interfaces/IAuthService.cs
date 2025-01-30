using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.Interfaces
{
    public interface IAuthService
    {
        public void LogOutUser();
        public void UpdatePassword(string password);
        public Guid GetUserIdByToken();
    }
}