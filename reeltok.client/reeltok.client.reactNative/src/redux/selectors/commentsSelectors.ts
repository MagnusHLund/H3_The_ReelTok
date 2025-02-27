import { createSelector } from 'reselect'
import { RootState } from '../store'

const selectCommentsState = (state: RootState) => state.comments

export const selectCommentsForVideo = createSelector(
  [selectCommentsState, (state: RootState, videoId) => videoId],
  (commentsState, videoId) =>
    commentsState.comments.filter((comment) => comment.videoId === videoId)
)

export const selectTotalVideoComments = createSelector(
  [selectCommentsState, (state: RootState, videoId) => videoId],
  (commentsState, videoId) =>
    commentsState.videoIdsWithCachedComments.find((cachedVideo) => cachedVideo.videoId === videoId)
      ?.totalVideoComments ?? 0
)
