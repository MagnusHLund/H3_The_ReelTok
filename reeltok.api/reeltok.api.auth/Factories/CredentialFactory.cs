using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Factories
{
    internal static class CredentialFactory
    {
        internal static UserCredentialsEntity CreateUserCredentialsEntity(
            Guid userId,
            HashedPasswordDetails hashedPasswordDetails
        )
        {
            return new UserCredentialsEntity(
                userId: userId,
                hashedPasswordDetails: hashedPasswordDetails
            );
        }
    }
}