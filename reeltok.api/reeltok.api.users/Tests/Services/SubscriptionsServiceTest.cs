// THIS CODE IS WECK AND I HAVE NO IDEA HOW TO FIX IT

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Moq;
// using reeltok.api.users.Entities;
// using reeltok.api.users.Interfaces.Repositories;
// using reeltok.api.users.Interfaces.Services;
// using reeltok.api.users.Services;
// using reeltok.api.users.ValueObjects;
// using Xunit;

// namespace reeltok.api.users.Tests.Services
// {
//     public class SubscriptionsServiceTest
//     {
//         private readonly Mock<ISubscriptionsRepository> _mockSubscriptionRepository;
//         private readonly Mock<IUsersService> _mockUsersService;
//         private readonly SubscriptionsService _subscriptionsService;

//         public SubscriptionsServiceTest()
//         {
//             _mockSubscriptionRepository = new Mock<ISubscriptionsRepository>();
//             _mockUsersService = new Mock<IUsersService>();
//             _subscriptionsService = new SubscriptionsService(_mockSubscriptionRepository.Object, _mockUsersService.Object);
//         }

//         #region Success Tests

//         [Fact]
//         public async Task GetSubscribersByUserIdAsync_ReturnsSubscribers()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             int pageNumber = 1;
//             byte pageSize = 10;
//             List<Guid> subscriberIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
//             List<UserEntity> userEntities = new List<UserEntity>
//             {
//                 new UserEntity(subscriberIds[0], new UserDetails("User 1", "Bio", "profileUrl"), new HiddenUserDetails("user1@example.com")),
//                 new UserEntity(subscriberIds[1], new UserDetails("User 2", "Bio", "profileUrl"), new HiddenUserDetails("user2@example.com"))
//             };

//             _mockSubscriptionRepository.Setup(x => x.GetSubscribersByUserIdAsync(userId, pageNumber, pageSize)).ReturnsAsync(subscriberIds);
//             _mockUsersService.Setup(x => x.GetUsersByIdsAsync(subscriberIds)).ReturnsAsync(userEntities);

//             // Act
//             List<ExternalUserEntity> result = await _subscriptionsService.GetSubscribersByUserIdAsync(userId, pageNumber, pageSize);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(2, result.Count);
//             Assert.Equal(subscriberIds[0], result[0].UserId);
//             Assert.Equal("User 1", result[0].UserDetails.Username);
//             Assert.Equal(subscriberIds[1], result[1].UserId);
//             Assert.Equal("User 2", result[1].UserDetails.Username);
//         }

//         [Fact]
//         public async Task GetSubscriptionsByUserIdAsync_ReturnsSubscriptions()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             int pageNumber = 1;
//             byte pageSize = 10;
//             List<Guid> subscriptionIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
//             List<UserEntity> userEntities = new List<UserEntity>
//             {
//                 new UserEntity(subscriptionIds[0], new UserDetails("Sub 1", "Bio", "profileUrl"), new HiddenUserDetails("sub1@example.com")),
//                 new UserEntity(subscriptionIds[1], new UserDetails("Sub 2", "Bio", "profileUrl"), new HiddenUserDetails("sub2@example.com"))
//             };

//             _mockSubscriptionRepository.Setup(x => x.GetSubscriptionsByUserIdAsync(userId, pageNumber, pageSize)).ReturnsAsync(subscriptionIds);
//             _mockUsersService.Setup(x => x.GetUsersByIdsAsync(subscriptionIds)).ReturnsAsync(userEntities);

//             // Act
//             List<ExternalUserEntity> result = await _subscriptionsService.GetSubscriptionsByUserIdAsync(userId, pageNumber, pageSize);

//             // Assert
//             Assert.NotNull(result);
//             Assert.Equal(2, result.Count);
//             Assert.Equal(subscriptionIds[0], result[0].UserId);
//             Assert.Equal("Sub 1", result[0].UserDetails.Username);
//             Assert.Equal(subscriptionIds[1], result[1].UserId);
//             Assert.Equal("Sub 2", result[1].UserDetails.Username);
//         }

//         [Fact]
//         public async Task SubscribeAsync_WithValidDetails_ReturnsTrue()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             Guid subscribingToUserId = Guid.NewGuid();
//             UserDetails userDetails = new UserDetails("User 1", "Bio", "profileUrl");
//             ExternalUserEntity externalUserEntity = new ExternalUserEntity(userId, userDetails);
//             SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, subscribingToUserId);

//             _mockUsersService.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(new UserEntity(userId, userDetails, new HiddenUserDetails("user1@example.com")));
//             _mockUsersService.Setup(x => x.GetUserByIdAsync(subscribingToUserId)).ReturnsAsync(new UserEntity(subscribingToUserId, userDetails, new HiddenUserDetails("user2@example.com")));
//             _mockSubscriptionRepository.Setup(x => x.AddUserToSubscriptionAsync(It.IsAny<SubscriptionEntity>())).ReturnsAsync(true);

//             // Act
//             bool result = await _subscriptionsService.SubscribeAsync(subscriptionDetails);

//             // Assert
//             Assert.True(result);
//         }

//         [Fact]
//         public async Task UnsubscribeAsync_WithValidDetails_ReturnsTrue()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             Guid subscribingToUserId = Guid.NewGuid();
//             SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, subscribingToUserId);

//             _mockUsersService.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(new UserEntity(userId, new UserDetails("User", "Bio", "profileUrl"), new HiddenUserDetails("user@example.com")));
//             _mockUsersService.Setup(x => x.GetUserByIdAsync(subscribingToUserId)).ReturnsAsync(new UserEntity(subscribingToUserId, new UserDetails("SubUser", "Bio", "profileUrl"), new HiddenUserDetails("sub@example.com")));
//             _mockSubscriptionRepository.Setup(x => x.RemoveUserFromSubscriptionAsync(userId, subscribingToUserId)).ReturnsAsync(true);

//             // Act
//             bool result = await _subscriptionsService.UnsubscribeAsync(subscriptionDetails);

//             // Assert
//             Assert.True(result);
//         }

//         #endregion

//         #region Failure Tests

//         [Fact]
//         public async Task SubscribeAsync_WithSameUserSubscription_ThrowsInvalidOperationException()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, userId);

//             // Act & Assert
//             InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _subscriptionsService.SubscribeAsync(subscriptionDetails));
//             Assert.Contains("User cannot subscribe to themselves!", exception.Message);
//         }

//         [Fact]
//         public async Task SubscribeAsync_WithNonExistingUser_ThrowsException()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             Guid subscribingToUserId = Guid.NewGuid();
//             SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, subscribingToUserId);

//             _mockUsersService.Setup(x => x.GetUserByIdAsync(userId)).ThrowsAsync(new Exception("User not found"));
//             _mockUsersService.Setup(x => x.GetUserByIdAsync(subscribingToUserId)).ReturnsAsync(new UserEntity(subscribingToUserId, new UserDetails("SubUser", "Bio", "profileUrl"), new HiddenUserDetails("sub@example.com")));

//             // Act & Assert
//             var exception = await Assert.ThrowsAsync<Exception>(() => _subscriptionsService.SubscribeAsync(subscriptionDetails));
//             Assert.Contains("User not found", exception.Message);
//         }

//         [Fact]
//         public async Task UnsubscribeAsync_WithNonExistingUser_ThrowsException()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             Guid subscribingToUserId = Guid.NewGuid();
//             SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, subscribingToUserId);

//             _mockUsersService.Setup(x => x.GetUserByIdAsync(userId)).ThrowsAsync(new Exception("User not found"));
//             _mockUsersService.Setup(x => x.GetUserByIdAsync(subscribingToUserId)).ReturnsAsync(new UserEntity(subscribingToUserId, new UserDetails("SubUser", "Bio", "profileUrl"), new HiddenUserDetails("sub@example.com")));

//             // Act & Assert
//             var exception = await Assert.ThrowsAsync<Exception>(() => _subscriptionsService.UnsubscribeAsync(subscriptionDetails));
//             Assert.Contains("User not found", exception.Message);
//         }

//         [Fact]
//         public async Task UnsubscribeAsync_WithInvalidSubscription_ThrowsException()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             Guid subscribingToUserId = Guid.NewGuid();
//             SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, subscribingToUserId);

//             _mockUsersService.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(new UserEntity(userId, new UserDetails("User", "Bio", "profileUrl"), new HiddenUserDetails("user@example.com")));
//             _mockUsersService.Setup(x => x.GetUserByIdAsync(subscribingToUserId)).ReturnsAsync(new UserEntity(subscribingToUserId, new UserDetails("SubUser", "Bio", "profileUrl"), new HiddenUserDetails("sub@example.com")));
//             _mockSubscriptionRepository.Setup(x => x.RemoveUserFromSubscriptionAsync(userId, subscribingToUserId)).ReturnsAsync(false);

//             // Act & Assert
//             var exception = await Assert.ThrowsAsync<Exception>(() => _subscriptionsService.UnsubscribeAsync(subscriptionDetails));
//             Assert.Contains("Failed to unsubscribe", exception.Message);
//         }

//         [Fact]
//         public async Task SubscribeAsync_WithRepositoryFailure_ThrowsException()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             Guid subscribingToUserId = Guid.NewGuid();
//             SubscriptionDetails subscriptionDetails = new SubscriptionDetails(userId, subscribingToUserId);

//             _mockUsersService.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(new UserEntity(userId, new UserDetails("User", "Bio", "profileUrl"), new HiddenUserDetails("user@example.com")));
//             _mockUsersService.Setup(x => x.GetUserByIdAsync(subscribingToUserId)).ReturnsAsync(new UserEntity(subscribingToUserId, new UserDetails("SubUser", "Bio", "profileUrl"), new HiddenUserDetails("sub@example.com")));
//             _mockSubscriptionRepository.Setup(x => x.AddUserToSubscriptionAsync(It.IsAny<SubscriptionEntity>())).ReturnsAsync(false);

//             // Act & Assert
//             var exception = await Assert.ThrowsAsync<Exception>(() => _subscriptionsService.SubscribeAsync(subscriptionDetails));
//             Assert.Contains("Failed to subscribe", exception.Message);
//         }

//         #endregion
//     }
// }
