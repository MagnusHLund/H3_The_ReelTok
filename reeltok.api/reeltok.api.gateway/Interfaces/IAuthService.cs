using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> LogOutUser();
        public Task<Guid> GetUserIdByToken();
    }
}