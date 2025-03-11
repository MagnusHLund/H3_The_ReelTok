using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using System;

namespace reeltok.api.recommendations.Tests.Factories
{
    public static class TestDataFactory
    {
        public static Guid CreateGuid()
        {
            return Guid.NewGuid();
        }

        public static CategoryEntity CreateCategoryEntity(CategoryType categoryType)
        {
            CategoryEntity categoryEntity = new CategoryEntity(categoryType);

            return categoryEntity;
        }

        public static UserEntity CreateUserEntity()
        {
            UserEntity userEntity = new UserEntity(CreateGuid());

            return userEntity;
        }

        public static VideoEntity CreateVideoEntity()
        {
            VideoEntity videoEntity = new VideoEntity(CreateGuid());

            return videoEntity;
        }

        public static WatchedVideoEntity CreateWatchedVideoEntity()
        {
            WatchedVideoEntity watchedVideoEntity = new WatchedVideoEntity(
                CreateGuid(),
                CreateGuid(),
                1,
                (uint) DateTime.UtcNow.Ticks
            );

            return watchedVideoEntity;
        }

        public static CategoryUserInterestEntity CreateCategoryUserInterestEntity()
        {
            CategoryUserInterestEntity categoryUserInterestEntity = new CategoryUserInterestEntity(CreateUserEntity(), 1);

            return categoryUserInterestEntity;
        }

        public static CategoryVideoCategoryEntity CreateCategoryVideoCategoryEntity()
        {
            CategoryVideoCategoryEntity categoryVideoCategoryEntity = new CategoryVideoCategoryEntity(CreateVideoEntity(), 1);

            return categoryVideoCategoryEntity;
        }

        public static List<CategoryEntity> CreateCategoryEntities(int count)
        {
            List<CategoryEntity> categories = new List<CategoryEntity>();
            for (int i = 0; i < count; i++)
            {
                categories.Add(CreateCategoryEntity((CategoryType) (i % 6)));
            }
            return categories;
        }

        public static List<UserEntity> CreateUserEntities(int count)
        {
            List<UserEntity> users = new List<UserEntity>();
            for (int i = 0; i < count; i++)
            {
                users.Add(CreateUserEntity());
            }
            return users;
        }

        public static List<VideoEntity> CreateVideoEntities(int count)
        {
            List<VideoEntity> videos = new List<VideoEntity>();
            for (int i = 0; i < count; i++)
            {
                videos.Add(CreateVideoEntity());
            }
            return videos;
        }

        public static List<Guid> CreateVideoIds(int count)
        {
            List<Guid> videoIds = new List<Guid>();
            for (int i = 0; i < count; i++)
            {
                videoIds.Add(CreateGuid());
            }
            return videoIds;
        }

        public static List<WatchedVideoEntity> CreateWatchedVideos(int count)
        {
            List<WatchedVideoEntity> watchedVideos = new List<WatchedVideoEntity>();
            for (int i = 0; i < count; i++)
            {
                watchedVideos.Add(CreateWatchedVideoEntity());
            }
            return watchedVideos;
        }

    }
}
