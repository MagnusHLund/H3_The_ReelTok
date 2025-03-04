import { createAsyncThunk } from '@reduxjs/toolkit'
import { setUploadedVideo, UploadedVideo } from '../slices/uploadSlice'

export const setUploadedVideoThunk = createAsyncThunk(
  'upload/setUploadedVideo',
  async (uploadedVideo: UploadedVideo, { dispatch }) => {
    console.log('test 2')
    dispatch(setUploadedVideo(uploadedVideo))
  }
)
