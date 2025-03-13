using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Factories
{
    internal static class VideoFactory
    {
        internal static VideoForFeedEntity CreateVideoForFeedEntity(
            VideoEntity videoEntity,
            VideoCreatorEntity videoCreatorDetails,
            VideoLikesEntity videoLikes
            )
        {
            if (videoEntity.VideoId != videoCreatorDetails.VideoId || videoEntity.VideoId != videoLikes.VideoId)
            {
                throw new ArgumentException("Video ids do not match, across videoEntity, videoCreatorDetails and videoLikes!");
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
            List<VideoEntity> videoEntity,
            List<VideoCreatorEntity> videoCreatorDetails,
            List<VideoLikesEntity> videoLikes)
        {
            Dictionary<Guid, VideoEntity> videoDict = videoEntity.ToDictionary(v => v.VideoId);
            Dictionary<Guid, VideoCreatorEntity> creatorDict = videoCreatorDetails.ToDictionary(c => c.VideoId);
            Dictionary<Guid, VideoLikesEntity> likesDict = videoLikes.ToDictionary(l => l.VideoId);

            List<VideoForFeedEntity> videosForFeed = new List<VideoForFeedEntity>();
            foreach (Guid videoId in videoIds)
            {
                if (videoDict.TryGetValue(videoId, out VideoEntity? video) &&
                    creatorDict.TryGetValue(videoId, out VideoCreatorEntity? creator) &&
                    likesDict.TryGetValue(videoId, out VideoLikesEntity? likes))
                {
                    VideoForFeedEntity videoForFeed = CreateVideoForFeedEntity(video, creator, likes);
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
