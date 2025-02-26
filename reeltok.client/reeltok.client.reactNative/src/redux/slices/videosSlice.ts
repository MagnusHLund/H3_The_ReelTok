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
  title: string
  description: string
  category: VideoRecommendationCategories
  likes: number
  hasLiked: boolean
  streamUrl: string
  uploadedAt: string
  creator: Creator
}

export type Creator = {
  username: string
  profileUrl: string
  profilePictureUrl: string
}

export interface VideosProps {
  videos: Video[] | undefined
}

// TODO: I dont know what i was thinking. Instead of having undefined videos, it can just be an empty array. Refactor initialState and the VideosProps, along with anywhere that is affected by this.
// ^^^^  See commentsSlice.ts, for an example of this
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
