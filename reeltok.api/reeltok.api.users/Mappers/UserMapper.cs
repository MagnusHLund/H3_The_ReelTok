using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.DTOs.UserRequestDTO;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.Entities;

namespace reeltok.api.users.Mappers
{
    internal static class UserMapper
    {
        internal static UserProfileData ToEntity(CreateUserRequestDto dto)
        {
            return new UserProfileData(
                Guid.NewGuid(), // Assuming a new user is created, so generating a new Guid
                new UserDetails(
                    dto.UserName,
                    dto.ProfileUrl,
                    dto.ProfilePictureUrl,
                    new HiddenUserDetails(dto.Email)
                )
            );
        }

        internal static CreateUserRequestDto ToDto(UserProfileData entity)
        {
            return new CreateUserRequestDto(
                entity.UserDetails.UserName,
                entity.UserDetails.ProfilePictureUrl,
                entity.UserDetails.ProfileUrl,
                entity.UserDetails.HiddenDetails.Email
            );
        }
    }
}