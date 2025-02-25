import { getRandomNumber } from './../../../node_modules/react-native-svg/src/lib/util'
import { createAsyncThunk } from '@reduxjs/toolkit'
import {
  addVideoToFeed,
  hasLikedVideo,
  Video,
  videoRecommendationCategories,
} from '../slices/videosSlice'

// TODO: Update this to use our APIs
// TODO: Handle more than one videos being returned and overall fix shit code inside the thunk
export const addVideoToFeedThunk = createAsyncThunk(
  'videos/addVideoToFeed',
  async (videosInFeed: Video[] | undefined, { dispatch }) => {
    const newVideos: Video[] = [
      {
        videoId: getRandomNumber().toString(),
        title: 'Mock video 1',
        description: 'Mock description 1',
        likes: 123,
        hasLiked: true,
        category: videoRecommendationCategories.Gaming,
        streamUrl:
          'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4',
        uploadedAt: new Date(Date.now()).toDateString(),
        creator: {
          username: 'Mock user1',
          profileUrl: 'http://local.profile.com',
          profilePictureUrl: 'http://profilePicture.profile.com',
        },
      },
      {
        videoId: getRandomNumber().toString(),
        title: 'Mock video 2',
        description: 'Mock description 2',
        likes: 321,
        hasLiked: false,
        category: videoRecommendationCategories.Gaming,
        streamUrl:
          'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4',
        uploadedAt: new Date(Date.now()).toDateString(),
        creator: {
          username: 'Mock user 2',
          profileUrl: 'http://local.profile.com',
          profilePictureUrl: 'http://profilePicture.profile.com',
        },
      },
    ]

    if (videosInFeed !== undefined && videosInFeed.length > 49) {
      videosInFeed.shift()
      videosInFeed.shift()
    }

    const newVideoFeed = videosInFeed ?? []
    newVideoFeed.push(newVideos[0])
    newVideoFeed.push(newVideos[1])

    dispatch(addVideoToFeed(newVideoFeed))
  }
)

// TODO: Call api to add/remove the like
export const hasLikedVideoThunk = createAsyncThunk(
  'videos/hasLikedVideo',
  async (video: Video, { dispatch }) => {
    const updatedVideo = {
      ...video,
      hasLiked: !video.hasLiked,
      likes: video.hasLiked ? video.likes - 1 : video.likes + 1,
    }

    dispatch(hasLikedVideo(updatedVideo))
  }
)
