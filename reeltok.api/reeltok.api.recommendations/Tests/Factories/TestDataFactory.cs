using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using System;

namespace reeltok.api.recommendations.Tests.Factories
{
    public static class TestDataFactory
    {
        public static CategoryEntity CreateCategoryEntity(CategoryType categoryType = CategoryType.Tech)
        {
            CategoryEntity categoryEntity = new CategoryEntity(categoryType);

            return categoryEntity;
        }

        public static CategoryUserInterestEntity CreateCategoryUserInterestEntity(UserEntity user, CategoryEntity category)
        {
            // This if is just cause the new instance is giving me yellow line all the time if i don't add this if statement
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            CategoryUserInterestEntity categoryUserInterestEntity = new CategoryUserInterestEntity(user, category.CategoryId);

            return categoryUserInterestEntity;
        }

        public static CategoryVideoCategoryEntity CreateCategoryVideoCategoryEntity(VideoEntity video, CategoryEntity category)
        {
            // This if is just cause the new instance is giving me yellow line all the time if i don't add this if statement
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            CategoryVideoCategoryEntity categoryVideoCategoryEntity = new CategoryVideoCategoryEntity(video, category.CategoryId);

            return categoryVideoCategoryEntity;
        }

        public static UserEntity CreateUserEntity(Guid? userId = null)
        {
            UserEntity userEntity = new UserEntity(userId ?? Guid.NewGuid());

            return userEntity;
        }

        public static VideoEntity CreateVideoEntity(Guid? videoId = null)
        {
            VideoEntity videoEntity = new VideoEntity(videoId ?? Guid.NewGuid());

            return videoEntity;
        }

        public static WatchedVideoEntity CreateWatchedVideoEntity(
            Guid? userId = null,
            Guid? videoId = null,
            ushort watchCount = 1,
            uint lastWatchedAt = 0
        )
        {
            WatchedVideoEntity watchedVideoEntity = new WatchedVideoEntity(
                userId ?? Guid.NewGuid(),
                videoId ?? Guid.NewGuid(),
                watchCount,
                lastWatchedAt
            );

            return watchedVideoEntity;
        }
    }
}
