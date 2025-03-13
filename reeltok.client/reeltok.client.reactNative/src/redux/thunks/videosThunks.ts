import { createAsyncThunk } from '@reduxjs/toolkit'
import {
  addVideoToFeed,
  hasLikedVideo,
  Video,
  VideoRecommendationCategories,
} from '../slices/videosSlice'
import { GetVideosForFeedRequestDto } from '../../DTOs/videos/GetVideosForFeed/GetVideosForFeedRequestDto'
import httpService from '../../services/httpService'
import { HttpMethod } from '../../services/httpService'
import { getRandomNumber } from './../../../node_modules/react-native-svg/src/lib/util'

// TODO: Update this to use our APIs
export const addVideoToFeedThunk = createAsyncThunk(
  'videos/addVideoToFeed',
  async (videosInFeed: Video[], { dispatch }) => {
    const newVideos: Video[] = [
      {
        videoId: getRandomNumber().toString(),

        creatorUserId: 'guidUserId3',

        title: 'Mock video 1',

        description: 'Mock description 1',

        likes: 123,

        hasLiked: true,

        category: VideoRecommendationCategories.Gaming,

        streamUrl:
          'https://cdn.viggle.ai/gras/f2736735-7bbd-4c8f-b0e7-2f84ec6aff0e.mp4?Expires=1740772331&KeyName=vigglecloudcdn2&Signature=BoAeGxk5B9bhr9a20ZaTiIDixc8=',

        uploadedAt: new Date(Date.now()).toDateString(),
      },

      {
        videoId: getRandomNumber().toString(),

        creatorUserId: 'guidUserId3',

        title: 'Mock video 2',

        description: 'Mock description 2',

        likes: 321,

        hasLiked: false,

        category: VideoRecommendationCategories.Gaming,

        streamUrl:
          'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4',

        uploadedAt: new Date(Date.now()).toDateString(),
      },
    ]

    let newVideoFeed = (videosInFeed ?? []).concat(newVideos)

    if (newVideoFeed.length > 50) {
      newVideoFeed = newVideoFeed.slice(newVideoFeed.length - 50)
    }

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
