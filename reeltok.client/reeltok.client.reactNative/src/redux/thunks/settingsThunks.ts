import { changeCategory, changeLanguage, LanguageName, LocaleCode } from '../slices/settingsSlice'
import { createAsyncThunk } from '@reduxjs/toolkit'

interface ChangeLanguagePayload {
  label: string
  value: string
}

export const changeLanguageThunk = createAsyncThunk(
  'settings/changeLanguageThunk',
  async (dropdownOption: ChangeLanguagePayload, { dispatch }) => {
    const languagePayload = {
      LanguageName: dropdownOption.label as LanguageName,
      locale: dropdownOption.value as LocaleCode,
    }

    dispatch(changeLanguage(languagePayload))
  }
)

export const changeCategoryThunk = createAsyncThunk(
  'settings/changeCategoryThunk',
  async (category: string, { dispatch }) => {
    dispatch(changeCategory(category))
  }
)
