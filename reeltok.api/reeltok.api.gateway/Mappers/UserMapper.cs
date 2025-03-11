using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Interfaces.DTOs;

namespace reeltok.api.gateway.Mappers
{
    internal static class UserMapper
    {
        internal static TDto ConvertUserProfileDataToResponseDto<TDto>(UserProfileData userProfileData)
            where TDto : IUserProfileDataDto, new()
        {
            return new TDto
            {
                UserId = userProfileData.UserId,
                Email = userProfileData.HiddenUserDetails.Email,
                Username = userProfileData.UserDetails.Username,
                ProfileUrl = userProfileData.UserDetails.ProfileUrl,
                ProfilePictureUrl = userProfileData.UserDetails.ProfilePictureUrl
            };
        }

        internal static TDto ConvertEditableUserDetailsToDto<TDto>(EditableUserDetails userDetails)
            where TDto : IEditableUserDetailsDto, new()
        {
            return new TDto
            {
                Username = userDetails.Username,
                Email = userDetails.Email
            };
        }

        internal static UserProfileData ConvertResponseDtoToUserProfileData(IUserProfileDataDto responseDto)
        {
            UserDetails userDetails = new UserDetails(
                username: responseDto.Username,
                profilePictureUrl: responseDto.ProfilePictureUrl,
                profileUrl: responseDto.ProfileUrl
                );

            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(email: responseDto.Email);
            return new UserProfileData(responseDto.UserId, userDetails, hiddenUserDetails);
        }
    }
}
