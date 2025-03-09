using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IUsersService
    {
        Task<CategoryType> GetUserInterestAsync(Guid userId);
        Task<CategoryType> AddInterestForUserAsync(Guid userId, CategoryType userInterest);
        Task<CategoryType> UpdateInterestForUserAsync(Guid userId, CategoryType newUserInterest);
    }
}
