using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    public class UsersService : IUsersService
    {
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;

        internal UsersService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public void LoginUser(string email, string password)
        {

        }
        public void CreateUser(string email, string password, string username)
        {

        }
        public UserProfileData GetUserProfileData(Guid userId)
        {
            return new UserProfileData();
        }
        public void UpdateUserDetails(UserProfileData profileData)
        {

        }
        public void UpdateProfilePicture(IFormFile image)
        {

        }
        public void DeleteUser(Guid userId)
        {

        }
        public void BlockUser(Guid userIdToBlock)
        {

        }
        public void UnblockUser(Guid userIdToUnblock)
        {

        }
        public List<UserDetails> GetBlockListByUser(Guid userId)
        {
            return new List<UserDetails>();
        }
        public List<Video> GetLikedVideosForUserProfile(Guid userId)
        {
            return new List<Video>();
        }
        public List<UserDetails> GetAllUserSubscriptionsForUser(Guid userId)
        {
            return new List<UserDetails>();
        }
        public List<UserDetails> GetAllSubscribingToUser(Guid userId)
        {
            return new List<UserDetails>();
        }
    }
}