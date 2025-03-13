using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Tests.Factories
{
    public static class TestDataFactory
    {
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        public static UserCredentialsEntity GenerateUserCredentialsEntity(Guid userId, string password, string salt)
        {
            HashedPasswordDetails hashedPasswordDetails = new HashedPasswordDetails(password, salt);
            UserCredentialsEntity userCredentialsEntity = new UserCredentialsEntity(userId, hashedPasswordDetails);

            return userCredentialsEntity;
        }
    }
}
