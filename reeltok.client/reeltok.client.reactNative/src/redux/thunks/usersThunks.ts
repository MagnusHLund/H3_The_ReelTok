import { createAsyncThunk } from '@reduxjs/toolkit'
import { UserDetails } from '../slices/usersSlice'

export const hasLikedVideoThunk = createAsyncThunk(
  'users/userLogin',
  async (user: UserDetails, { dispatch }) => {
    //dispatch(hasLikedVideo(updatedVideo))
  }
)
