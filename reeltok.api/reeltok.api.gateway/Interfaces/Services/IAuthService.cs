namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> LogOutUser();
        Task<Guid> GetUserIdByToken();
    }
}
