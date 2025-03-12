namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> LogOutUserAsync();
        Task<Guid> GetUserIdByAccessTokenAsync();
    }
}
