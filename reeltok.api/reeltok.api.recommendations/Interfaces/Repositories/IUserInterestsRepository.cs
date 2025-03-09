using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IUserInterestsRepository
    {
        Task<CategoryEntity> GetUserInterestAsync(Guid userId);
        Task<uint> AddUserInterestAsync(CategoryUserInterestEntity categoryUserInterestEntity);
        Task<uint> UpdateUserInterestAsync(UserEntity user, uint newCategoryId);
        Task RemoveExistingUserInterestAsync(Guid userId);
    }
}