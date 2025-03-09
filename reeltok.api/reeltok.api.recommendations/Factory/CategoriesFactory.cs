using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Mappers;

namespace reeltok.api.recommendations.Factory
{
    internal static class CategoriesFactory
    {
        internal static CategoryEntity CreateCategoryEntity(CategoryType category)
        {
            uint categoryId = CategoryMapper.ConvertCategoryTypeToCategoryId(category);
            CategoryEntity categoryEntity = new CategoryEntity(categoryId, category);

            return categoryEntity;
        }

        internal static CategoryUserInterestEntity CreateCategoryUserInterestEntity(
            CategoryEntity categoryEntity,
            UserEntity userEntity
        )
        {
            return new CategoryUserInterestEntity(userEntity, categoryEntity);
        }
    }
}