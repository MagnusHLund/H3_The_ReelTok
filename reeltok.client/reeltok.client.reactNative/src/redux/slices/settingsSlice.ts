import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type LanguageName = 'Danish' | 'English'
export type LocaleCode = 'da_DK' | 'en_GB'

export type Language = { LanguageName: LanguageName; locale: LocaleCode }

export interface SettingsProps {
  language: Language
  selectedCategory: string
  username: string // New field for username
  email: string // New field for email
}

const initialState: SettingsProps = {
  language: { LanguageName: 'English', locale: 'en_GB' },
  selectedCategory: '',
  username: '',
  email: '',
}

const settingsSlice = createSlice({
  name: 'settings',
  initialState,
  reducers: {
    changeLanguage: (state, action: PayloadAction<Language>) => {
      state.language = action.payload
    },
    changeCategory: (state, action: PayloadAction<string>) => {
      state.selectedCategory = action.payload
    },
    changeUsername: (state, action: PayloadAction<string>) => {
      state.username = action.payload
    },
    changeEmail: (state, action: PayloadAction<string>) => {
      state.email = action.payload
    },
  },
})

export const { changeLanguage, changeCategory, changeUsername, changeEmail } = settingsSlice.actions
export default settingsSlice.reducer

