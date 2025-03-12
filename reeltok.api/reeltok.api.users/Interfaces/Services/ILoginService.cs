using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces.Services
{
    public interface ILoginService
    {
        Task<UserWithInterestEntity> LoginUserAsync(string email, string password);
    }
}
