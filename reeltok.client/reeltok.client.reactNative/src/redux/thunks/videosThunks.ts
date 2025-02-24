import { getRandomNumber } from './../../../node_modules/react-native-svg/src/lib/util'
import { createAsyncThunk } from '@reduxjs/toolkit'
import { addVideoToFeed, Video, videoRecommendationCategories } from '../slices/videosSlice'

// TODO: Update this to use our APIs
// TODO: Handle more than one videos being returned and overall fix shit code inside the thunk
export const addVideoToFeedThunk = createAsyncThunk(
  'videos/addVideoToFeed',
  async (videosInFeed: Video[] | undefined, { dispatch }) => {
    const newVideos: Video[] = [
      {
        videoId: getRandomNumber().toString(),
        title: 'Mock video',
        description: 'Mock description',
        likes: 123,
        hasLiked: true,
        category: videoRecommendationCategories.Gaming,
        streamUrl:
          'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4',
        uploadedAt: new Date(Date.now()),
        creator: {
          username: 'Mock user',
          profileUrl: 'http://local.profile.com',
          profilePictureUrl: 'http://profilePicture.profile.com',
        },
      },
      {
        videoId: getRandomNumber().toString(),
        title: 'Mock video',
        description: 'Mock description',
        likes: 123,
        hasLiked: true,
        category: videoRecommendationCategories.Gaming,
        streamUrl:
          'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4',
        uploadedAt: new Date(Date.now()),
        creator: {
          username: 'Mock user',
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
