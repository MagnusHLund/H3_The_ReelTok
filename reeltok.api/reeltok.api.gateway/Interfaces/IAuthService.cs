namespace reeltok.api.gateway.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> LogOutUser();
        public Task<Guid> GetUserIdByToken();
    }
}