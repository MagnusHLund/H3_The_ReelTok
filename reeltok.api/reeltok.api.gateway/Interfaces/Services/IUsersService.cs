using System;
using System.Threading.Tasks;
using reeltok.api.gateway.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using reeltok.api.gateway.Entities.Users;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IUsersService
    {
        Task<UserEntity> LoginUserAsync(string email, string password);
        Task<UserEntity> CreateUserAsync(string email, string username, string password, byte userInterest);
        Task<ExternalUserEntity> GetUserById(Guid userId);
        Task<UserEntity> UpdateUserDetailsAsync(string? username, string? email, CategoryType? interest);
        Task<UserEntity> UpdateProfilePictureAsync(IFormFile image);
        Task<List<ExternalUserEntity>> GetUserSubscriptionsAsync(Guid userId, uint pageNumber, byte pageSize);
        Task<List<ExternalUserEntity>> GetUserSubscribersAsync(Guid userId, uint pageNumber, byte pageSize);
        Task<bool> SubscribeToUserAsync(Guid subscribeToUserId);
        Task<bool> UnsubscribeToUserAsync(Guid subscribeToUserId);
    }
}
