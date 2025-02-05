using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.DTOs.Interfaces;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Mappers
{
    internal static class UserMapper
    {
        internal static T ConvertUserProfileDataToResponseDto<T>(UserProfileData userProfileData) where T : IUserProfileDataDto, new()
        {
            return new T
            {
                UserId = userProfileData.UserId,
                Email = userProfileData.HiddenUserDetails.Email,
                Username = userProfileData.UserDetails.Username,
                ProfileUrl = userProfileData.UserDetails.ProfileUrl,
                ProfilePictureUrl = userProfileData.UserDetails.ProfilePictureUrl
            };
        }

        internal static T ConvertEditableUserDetailsToDto<T>(EditableUserDetails userDetails) where T : IEditableUserDetailsDto, new()
        {
            return new T
            {
                Username = userDetails.Username,
                Email = userDetails.Email
            };
        }
    }
}