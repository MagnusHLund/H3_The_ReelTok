namespace reeltok.api.gateway.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LogOutUser();
        Task<Guid> GetUserIdByToken();
    }
}
