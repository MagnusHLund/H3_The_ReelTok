using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Services;

namespace reeltok.api.users.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsersService _usersService;
        private readonly IExternalApiService _externalApiService;

        public LoginService(IUsersService usersService, IExternalApiService externalApiService)
        {
            _usersService = usersService;
            _externalApiService = externalApiService;
        }

        public async Task<UserEntity> LoginUserAsync(string email, string password)
        {
            UserEntity user = await _usersService.GetUserByEmail(email).ConfigureAwait(false);
            await _externalApiService.LoginUserInAuthApiAsync(user.UserId, password).ConfigureAwait(false);

            return user;
        }
    }
}