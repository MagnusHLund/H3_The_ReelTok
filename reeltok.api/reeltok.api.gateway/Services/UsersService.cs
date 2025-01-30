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
        private readonly AuthService _authService;
        private readonly GatewayService _gatewayService;

        internal UsersService(AuthService authService, GatewayService gatewayService)
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

        }
        public List<Video> GetLikedVideosForUserProfile(Guid userId)
        {

        }
        public List<UserDetails> GetAllUserSubscriptionsForUser(Guid userId)
        {

        }
        public List<UserDetails> GetAllSubscribingToUser(Guid userId)
        {

        }
    }
}