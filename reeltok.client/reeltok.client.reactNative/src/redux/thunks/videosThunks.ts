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

// TODO: Update this to use our APIs
export const addVideoToFeedThunk = createAsyncThunk(
  'videos/addVideoToFeed',
  async (videosInFeed: Video[], { dispatch }) => {
    
    const method: HttpMethod = 'GET'
    const url: URL = new URL('https://api.reeltok.site/api/videos/feed') 
    const body: GetVideosForFeedRequestDto = {
      userId: "",
      amount: 2
    } 

    const newVideos = await httpService<GetVideosForFeedRequestDto>({method, url, body}) 

    let newVideoFeed = (videosInFeed ?? []).concat(newVideos?.data)

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
