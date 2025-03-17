import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export enum VideoRecommendationCategories {
  Gaming = 0,
  Tech = 1,
  Dance = 2,
  Fight = 3,
  Sport = 4,
  Comedy = 5,
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
  videos: [],
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
