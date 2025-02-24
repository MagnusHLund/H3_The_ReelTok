import { createSlice, PayloadAction } from '@reduxjs/toolkit'

export type LanguageName = 'Danish' | 'English'
export type LocaleCode = 'da_DK' | 'en_GB'

export type Language = { LanguageName: LanguageName; locale: LocaleCode }

export interface SettingsProps {
  language: Language
  selectedCategory: string
}

const initialState: SettingsProps = {
  language: { LanguageName: 'English', locale: 'en_GB' },
  selectedCategory: '',
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
  },
})

export const { changeLanguage, changeCategory } = settingsSlice.actions
export default settingsSlice.reducer

