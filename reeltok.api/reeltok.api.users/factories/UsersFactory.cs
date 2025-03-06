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
                profileUrlPath: null,
                profilePictureUrlPath: null
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

        internal static UserEntity UpdateUserEntityEmail(UserEntity user, string email)
        {
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(
                email: email
            );

            return new UserEntity(
                userId: user.UserId,
                userDetails: user.UserDetails,
                hiddenUserDetails: hiddenUserDetails
            );
        }

        internal static UserEntity UpdateUserEntityUsername(UserEntity user, string username)
        {
            UserDetails userDetails = new UserDetails(
                username: username,
                profileUrlPath: user.UserDetails.ProfileUrlPath,
                profilePictureUrlPath: user.UserDetails.ProfilePictureUrlPath
            );

            return new UserEntity(
                userId: user.UserId,
                userDetails: userDetails,
                hiddenUserDetails: user.HiddenUserDetails
            );
        }
    }
}