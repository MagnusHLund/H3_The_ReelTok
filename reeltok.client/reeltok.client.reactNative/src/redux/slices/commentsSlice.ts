import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type Comment = {
  commentId: string
  videoId: string
  userId: string
  commentText: string
}

export type cachedVideoComments = {
  videoId: string
  totalVideoComments: number
}

export interface CommentsProps {
  comments: Comment[]
  videoIdsWithCachedComments: cachedVideoComments[]
}

const initialState: CommentsProps = {
  comments: [],
  videoIdsWithCachedComments: [],
}

const commentsSlice = createSlice({
  name: 'comments',
  initialState,
  reducers: {
    loadCommentsForVideo: (state, action: PayloadAction<Comment[]>) => {
      state.comments = action.payload
    },
  },
})

export const { loadCommentsForVideo } = commentsSlice.actions
export default commentsSlice.reducer
