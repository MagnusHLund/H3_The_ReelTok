using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.DTOs.UserRequestDTO;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.Entities;
using reeltok.api.users.DTOs.UserResponseDTO;

namespace reeltok.api.users.Mappers
{
    public static class UserMapper
    {
        public static Users ToUsersFromCreateDTO(this CreateUserRequestDto dto)
        {
            return new Users(
                Guid.Empty,
                new UserDetails(
                    dto.UserName,
                    "/MOFOS",
                    "/MOFOS",
                    new HiddenUserDetails(dto.Email)
                )
            );
        }

        public static ReturnCreateUserResponseDTO ToReturnCreateUserResponseDTO(this Users user)
        {
            return new ReturnCreateUserResponseDTO(
                user.UserId,
                user.UserDetails.UserName,
                user.UserDetails.ProfilePictureUrl,
                user.UserDetails.ProfileUrl,
                user.UserDetails.HiddenDetails.Email
            );
        }

        public static UserDetails ToUserDetailsFromUpdateDTO(this UpdateUserRequestDto dto)
        {
            return new UserDetails(
                dto.UserName,
                "/UpdatedProfileUrl", // Update as needed
                "/UpdatedProfilePictureUrl", // Update as needed
                new HiddenUserDetails(dto.Email)
            );
        }
    }
}