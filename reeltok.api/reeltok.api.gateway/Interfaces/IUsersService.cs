using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface IUsersService
    {
        public void LoginUser(string email, string password);
        public void CreateUser(string email, string password, string username);
        public UserProfileData GetUserProfileData(Guid userId);
        public void UpdateUserDetails(UserProfileData profileData);
        public void UpdateProfilePicture(IFormFile image);
        public void DeleteUser(Guid userId);
        public void BlockUser(Guid userIdToBlock);
        public void UnblockUser(Guid userIdToUnblock);
        public List<UserDetails> GetBlockListByUser(Guid userId);
        public List<Video> GetLikedVideosForUserProfile(Guid userId);
        public List<UserDetails> GetAllUserSubscriptionsForUser(Guid userId);
        public List<UserDetails> GetAllSubscribingToUser(Guid userId);
    }
}