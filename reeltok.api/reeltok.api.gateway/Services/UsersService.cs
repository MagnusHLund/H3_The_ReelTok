using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class UsersService : IUsersService
    {
        private const string UsersMicroServiceBaseUrl = "http://localhost:5001/users";
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;

        internal UsersService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public async Task<UserProfileData> LoginUser(string email, string password)
        {
            return new UserProfileData(Guid.Empty, new UserDetails("", "", ""), new HiddenUserDetails(""));
        }
        public async Task<UserProfileData> CreateUser(string email, string username, string password)
        {
            return new UserProfileData(Guid.Empty, new UserDetails("", "", ""), new HiddenUserDetails(""));
        }
        public async Task<UserProfileData> GetUserProfileData(Guid userId)
        {
            return new UserProfileData(Guid.Empty, new UserDetails("", "", ""), new HiddenUserDetails(""));
        }
        public async Task<UserDetails> UpdateUserDetails(UserProfileData profileData)
        {
            return new UserDetails("", "", "");
        }
        public async Task<IFormFile> UpdateProfilePicture(IFormFile image) // TODO: Maybe theres a better return type than this?
        {
            return image;
        }
        public async Task<List<UserDetails>> GetAllUserSubscriptionsForUser(Guid userId)
        {
            return new List<UserDetails>();
        }
        public async Task<List<UserDetails>> GetAllSubscribingToUser(Guid userId)
        {
            return new List<UserDetails>();
        }
    }
}