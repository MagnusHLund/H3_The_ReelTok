import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import { DropdownOption } from '../../components/input/CustomDropdown'

export type UploadedVideo = {
  title: string
  description: string
  category: DropdownOption
  fileUri: string
}

export interface UploadProps {
  video: UploadedVideo
}

const initialState: UploadProps = {
  video: {
    title: '',
    description: '',
    category: { label: '', value: '' },
    fileUri: '',
  },
}

const uploadSlice = createSlice({
  name: 'upload',
  initialState,
  reducers: {
    setUploadedVideo: (state, action: PayloadAction<UploadedVideo>) => {
      state.video = action.payload
    },
  },
})

export const { setUploadedVideo } = uploadSlice.actions
export default uploadSlice.reducer
