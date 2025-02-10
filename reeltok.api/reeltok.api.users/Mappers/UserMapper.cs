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
                    string.Empty, // ProfileUrl placeholder
                    string.Empty, // ProfilePictureUrl placeholder
                    new HiddenUserDetails(dto.Email)
                )
            );
        }

        public static ReturnCreateUserResponseDTO ToReturnCreateUserResponseDTO(Users user)
        {
            return new ReturnCreateUserResponseDTO(
                user.UserId,
                user.UserDetails.UserName,
                user.UserDetails.ProfilePictureUrl,
                user.UserDetails.ProfileUrl,
                user.UserDetails.HiddenDetails.Email
            );
        }

    }
}