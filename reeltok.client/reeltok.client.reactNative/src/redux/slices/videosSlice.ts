import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export enum videoRecommendationCategories {
  Gaming,
  Tech,
  Dance,
  Fight,
  Sport,
  Comedy,
}

export type Video = {
  videoId: string
  title: string
  description: string
  category: videoRecommendationCategories
  likes: number
  hasLiked: boolean
  streamUrl: string
  uploadedAt: Date
  creator: creator
}

export type creator = {
  username: string
  profileUrl: string
  profilePictureUrl: string
}

export interface VideosProps {
  videos: Video[] | undefined
}

const initialState: VideosProps = {
  videos: undefined,
}

const videosSlice = createSlice({
  name: 'videos',
  initialState,
  reducers: {
    addVideoToFeed: (state, action: PayloadAction<Video[]>) => {
      state.videos = action.payload
    },
  },
})

export const { addVideoToFeed } = videosSlice.actions
export default videosSlice.reducer
