using reeltok.api.auth.Entities;

namespace reeltok.api.auth.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<UserCredentialsEntity> CreateUser(UserCredentialsEntity userCredentials);
        Task DeleteUser(Guid userId);
        Task<UserCredentialsEntity> GetUserCredentialsByUserId(Guid userId);
        Task<bool> DoesUserExist(Guid userId);
    }
}
