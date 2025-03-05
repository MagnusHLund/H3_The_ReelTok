using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.factories
{
    internal static class UsersFactory
    {
        internal static UserEntity CreateUserEntity(string username, string email)
        {
            UserDetails userDetails = new UserDetails(
                username: username,
                profileUrl: string.Empty,
                profilePictureUrl: null
            );

            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(
                email: email
            );

            return new UserEntity(
                userId: Guid.Empty,
                userDetails: userDetails,
                hiddenUserDetails: hiddenUserDetails
            );
        }
    }
}