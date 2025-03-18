import { createAsyncThunk } from '@reduxjs/toolkit'
import { UserDetails, addUsers } from '../slices/usersSlice'

export const addUserFromVideoThunk = createAsyncThunk(
  'users/addUsers',
  async (videos: any, { dispatch }) => {
    let videoCreatorList: UserDetails[] = []
    videos.forEach((video) => {
      const videoCreator = video.VideoCreator
      if (videoCreator && !videoCreatorList.some((user) => user.userId === videoCreator.UserId)) {
        videoCreatorList.push({
          userId: videoCreator.UserId,
          username: videoCreator.UserDetails.Username,
          profilePictureUrl: videoCreator.UserDetails.ProfilePictureUrlPath ?? '',
        })
      }
    })
    console.log('Adding users:', videoCreatorList)
    dispatch(addUsers(videoCreatorList))
  }
)
