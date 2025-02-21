import { configureStore } from '@reduxjs/toolkit'
import LanguageSlice from './slices/languageSlice'

export interface RootState {
  language: ReturnType<typeof LanguageSlice>
}

const store = configureStore({
  reducer: { language: LanguageSlice },
})

export default store
