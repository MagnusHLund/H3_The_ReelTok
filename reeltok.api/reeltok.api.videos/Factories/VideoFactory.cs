using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Factories
{
    internal static class VideoFactory
    {
        internal static VideoForFeedEntity CreateVideoForFeedEntity(
            VideoEntity videoEntity,
            UserEntity videoCreatorDetails,
            VideoLikesEntity videoLikes
            )
        {
            if (videoEntity.UserId != videoCreatorDetails.UserId || videoEntity.VideoId != videoLikes.VideoId)
            {
                throw new ArgumentException("Entities do not match each other!");
            }

            VideoDetails videoDetails = new VideoDetails(
                title: videoEntity.Title,
                description: videoEntity.Description
            );

            UserEntity videoCreator = new UserEntity(
                userId: videoCreatorDetails.UserId,
                userDetails: videoCreatorDetails.UserDetails
            );

            return new VideoForFeedEntity(
                videoId: videoEntity.VideoId,
                videoDetails: videoDetails,
                videoLikes: videoLikes.VideoLikes,
                videoCreator: videoCreator,
                streamPath: videoEntity.StreamPath,
                uploadedAt: videoEntity.UploadedAt
            );
        }

        internal static List<VideoForFeedEntity> CreateVideoForFeedEntityList(
            List<Guid> videoIds,
            List<VideoEntity> videoEntities,
            List<UserEntity> videoCreatorDetails,
            List<VideoLikesEntity> videoLikesEntity)
        {
            Dictionary<Guid, VideoEntity> videoDict = videoEntities.ToDictionary(v => v.VideoId);
            Dictionary<Guid, UserEntity> creatorDict = videoCreatorDetails.ToDictionary(c => c.UserId);
            Dictionary<Guid, VideoLikesEntity> likesDict = videoLikesEntity.ToDictionary(l => l.VideoId);

            List<VideoForFeedEntity> videosForFeed = new List<VideoForFeedEntity>();
            foreach (Guid videoId in videoIds)
            {
                if (videoDict.TryGetValue(videoId, out VideoEntity? video) &&
                    creatorDict.TryGetValue(video.UserId, out UserEntity? creator))
                {
                    VideoLikesEntity videoLikes = likesDict.TryGetValue(videoId, out VideoLikesEntity? likes)
                        ? likes
                        : new VideoLikesEntity(videoId, new VideoLikes(0, false));

                    VideoForFeedEntity videoForFeed = CreateVideoForFeedEntity(video, creator, videoLikes);
                    videosForFeed.Add(videoForFeed);
                }
            }

            return videosForFeed;
        }

        internal static List<VideoLikesEntity> CreateVideoLikesEntityList(
            List<Guid> videoIds,
            List<HasUserLikedVideoEntity> hasUserLikedVideoEntities,
            List<TotalVideoLikesEntity> totalVideoLikesEntities
        )
        {
            Dictionary<Guid, HasUserLikedVideoEntity> userLikedDict = hasUserLikedVideoEntities.
                ToDictionary(hasUserLiked => hasUserLiked.VideoId);

            Dictionary<Guid, TotalVideoLikesEntity> totalLikesDict = totalVideoLikesEntities.ToDictionary(total => total.VideoId);

            List<VideoLikesEntity> videoLikes = new List<VideoLikesEntity>();
            foreach (Guid videoId in videoIds)
            {
                if (userLikedDict.TryGetValue(videoId, out HasUserLikedVideoEntity? hasUserLiked) &&
                    totalLikesDict.TryGetValue(videoId, out TotalVideoLikesEntity? totalLike))
                {
                    VideoLikes videoLikesValue = new VideoLikes(totalLike.TotalLikes, hasUserLiked.HasUserLikedVideo);
                    VideoLikesEntity videoLikesEntity = new VideoLikesEntity(videoId, videoLikesValue);
                    videoLikes.Add(videoLikesEntity);
                }
            }

            return videoLikes;
        }
    }
}
