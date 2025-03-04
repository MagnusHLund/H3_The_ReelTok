import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type UploadedVideo = {
  fileUri: string
}

export interface UploadProps {
  video: UploadedVideo
}

const initialState: UploadProps = {
  video: {
    fileUri: '',
  },
}

const uploadSlice = createSlice({
  name: 'upload',
  initialState,
  reducers: {
    setUploadedVideo: (state, action: PayloadAction<UploadedVideo>) => {
      console.log('test 3: ' + action.payload.fileUri)
      state.video = action.payload
    },
  },
})

export const { setUploadedVideo } = uploadSlice.actions
export default uploadSlice.reducer
