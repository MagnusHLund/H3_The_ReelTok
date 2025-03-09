using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IUserInterestsRepository
    {
        Task<CategoryEntity> GetUserInterestAsync(Guid userId);
        Task<CategoryUserInterestEntity> AddUserInterestAsync(CategoryUserInterestEntity categoryUserInterestEntity);
    }
}