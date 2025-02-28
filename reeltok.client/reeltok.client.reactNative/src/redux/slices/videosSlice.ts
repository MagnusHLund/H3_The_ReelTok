import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export enum VideoRecommendationCategories {
  Gaming,
  Tech,
  Dance,
  Fight,
  Sport,
  Comedy,
}

export type Video = {
  videoId: string
  creatorUserId: string
  title: string
  description: string
  category: VideoRecommendationCategories
  likes: number
  hasLiked: boolean
  streamUrl: string
  uploadedAt: string
}

export interface VideosProps {
  videos: Video[]
}

const initialState: VideosProps = {
  videos: [
    {
      videoId: 'guidVideoId1',
      creatorUserId: 'guidUserId3',
      title: 'Mock video 1',
      description: 'Mock description 1',
      likes: 123,
      hasLiked: true,
      category: VideoRecommendationCategories.Gaming,
      streamUrl:
        'http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4',
      uploadedAt: new Date(Date.now()).toDateString(),
    },
    {
      videoId: 'guidVideoId2',
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
  ],
}

const videosSlice = createSlice({
  name: 'videos',
  initialState,
  reducers: {
    addVideoToFeed: (state, action: PayloadAction<Video[]>) => {
      state.videos = action.payload
    },
    hasLikedVideo: (state, action: PayloadAction<Video>) => {
      state.videos?.find((video) => {
        if (video.videoId === action.payload.videoId) {
          video.hasLiked = action.payload.hasLiked
          video.likes = action.payload.likes
        }
      })
    },
  },
})

export const { addVideoToFeed, hasLikedVideo } = videosSlice.actions
export default videosSlice.reducer
