using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.DTOs.UserRequests;
using reeltok.api.users.ValueObjects;
using reeltok.api.users.Entities;
using reeltok.api.users.DTOs.UserResponses;

namespace reeltok.api.users.Mappers
{
    public static class UserMapper
    {
        public static User ToUsersFromCreateDTO(this CreateUserRequestDto dto)
        {
            return new User(
                Guid.Empty,
                new UserDetails(
                    dto.UserName,
                    "/MOFOS",
                    "/MOFOS",
                    new HiddenUserDetails(dto.Email)
                )
            );
        }

        public static ReturnCreateUserResponseDTO ToReturnCreateUserResponseDTO(this User user)
        {
            return new ReturnCreateUserResponseDTO
            {
                UserId = user.UserId,
                UserName = user.UserDetails.UserName,
                ProfileUrl = user.UserDetails.ProfilePictureUrl,
                ProfilePictureUrl = user.UserDetails.ProfileUrl,
                Email = user.UserDetails.HiddenDetails.Email
            };
        }

        public static UserDetails ToUserDetailsFromUpdateDTO(this UpdateUserRequestDto dto, User existingUser)
        {
            return new UserDetails(
                dto.UserName,
                existingUser.UserDetails.ProfileUrl,
                existingUser.UserDetails.ProfilePictureUrl,
                new HiddenUserDetails(dto.Email)
            );
        }
    }
}
