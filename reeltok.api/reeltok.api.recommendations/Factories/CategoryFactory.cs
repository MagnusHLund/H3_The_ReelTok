using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Factories
{
    internal static class CategoryFactory
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
            uint categoryId = CategoryMapper.ConvertCategoryTypeToCategoryId(categoryEntity.Category);
            return new CategoryUserInterestEntity(userEntity, categoryId);
        }

        internal static CategoryVideoCategoryEntity CreateCategoryVideoInterestEntity(
            CategoryEntity categoryEntity,
            VideoEntity videoEntity
        )
        {
            uint categoryId = CategoryMapper.ConvertCategoryTypeToCategoryId(categoryEntity.Category);
            return new CategoryVideoCategoryEntity(videoEntity, categoryId);
        }
    }
}