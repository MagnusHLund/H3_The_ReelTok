using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.DTOs.UserRequests;
using reeltok.api.users.DTOs.UserResponses;
using reeltok.api.users.DTOs.GetSubscriptions;

namespace reeltok.api.users.Mappers
{
    public static class UserMapper
    {
        // TODO: Refactor mapper method names. "ConvertXtoY" format.
        public static UserEntity ToUsersFromCreateDTO(this CreateUserRequestDto dto)
        {
            UserDetails userDetails = new UserDetails(
                    dto.Username,
                    "/MOFOS",
                    "/MOFOS"
                );

            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(
                    email: dto.Email
                );

            return new UserEntity(
                userId: Guid.Empty,
                userDetails: userDetails,
                hiddenUserDetails: hiddenUserDetails
            );
        }

        public static ReturnCreateUserResponseDTO ToReturnCreateUserResponseDTO(UserEntity user)
        {
            return new ReturnCreateUserResponseDTO
            {
                UserId = user.UserId,
                Username = user.UserDetails.Username,
                ProfileUrl = user.UserDetails.ProfilePictureUrl,
                ProfilePictureUrl = user.UserDetails.ProfileUrl,
                Email = user.HiddenDetails.Email
            };
        }

        public static UserDetails ToUserDetailsFromUpdateDTO(this UpdateUserRequestDto dto, UserEntity existingUser)
        {
            return new UserDetails(
                dto.Username,
                existingUser.UserDetails.ProfileUrl,
                existingUser.UserDetails.ProfilePictureUrl,
                new HiddenUserDetails(dto.Email)
            );
        }

        public static GetSubscriptionsResponseDto CovertUserEntitiesToGetSubscriptionsResponseDto(List<UserEntity> users)
        {
            List<UserDetails> userDetailsList = users
                .Select(user =>
                {
                    ReturnCreateUserResponseDTO userDetailsDto = ToReturnCreateUserResponseDTO(user);
                    return new UserDetails(
                        username: userDetailsDto.Username,
                        profileUrl: userDetailsDto.ProfileUrl,
                        profilePictureUrl: userDetailsDto.ProfilePictureUrl
                    );
                })
                .ToList();

            return new GetSubscriptionsResponseDto(users);
        }
    }
}
