import { createAsyncThunk } from '@reduxjs/toolkit'
import { setUploadedVideo, UploadedVideo } from '../slices/uploadSlice'
import httpService, { HttpMethod, PayloadType } from '../../services/httpService'
import { UploadVideoRequestDto } from '../../DTOs/videos/UploadVideo/UploadVideoRequestDto'

export const setUploadedVideoThunk = createAsyncThunk(
  'upload/setUploadedVideo',
  async (uploadedVideo: UploadedVideo, { dispatch }) => {
    const httpMethod: HttpMethod = 'POST'
    const url: string = 'api/videos/'
    const body: UploadVideoRequestDto = {
      title: uploadedVideo.title,
      description: uploadedVideo.description,
      category: uploadedVideo.category.value,
      video: uploadedVideo.fileUri  
    }
    const payloadType: PayloadType = 'FormDataBody'

    const newUploadedVideo = await httpService<UploadVideoRequestDto>({httpMethod, url, body, payloadType })

    dispatch(setUploadedVideo(newUploadedVideo?.data))
  }
)
