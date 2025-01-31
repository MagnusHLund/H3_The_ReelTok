using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities;

namespace reeltok.api.users.Interfaces
{
    public interface IUserService
    {
        Task CreateAsync(UserProfileData user);
        Task<UserProfileData?> GetByIdAsync(Guid userId);
        Task UpdateUserAsync(UserProfileData user);

        Task SubscribeAsync(Guid userId, Guid subscribeUserId);
        Task UnsubscribeAsync(Guid userId, Guid subscribeUserId);

        Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId);
        Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId);

    }
}