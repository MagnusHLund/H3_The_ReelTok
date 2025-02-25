import { DropdownOption } from './../../components/input/CustomDropdown'
import {
  changeUsername,
  changeEmail,
  changeCategory,
  changeLanguage,
  LanguageName,
  LocaleCode,
} from '../slices/settingsSlice'
import { createAsyncThunk } from '@reduxjs/toolkit'

export const changeLanguageThunk = createAsyncThunk(
  'settings/changeLanguageThunk',
  async (dropdownOption: DropdownOption, { dispatch }) => {
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

export const changeUsernameThunk = createAsyncThunk(
  'settings/changeUsernameThunk',
  async (newUsername: string, { dispatch }) => {
    dispatch(changeUsername(newUsername))
  }
)

export const changeEmailThunk = createAsyncThunk(
  'settings/changeEmailThunk',
  async (newEmail: string, { dispatch }) => {
    dispatch(changeEmail(newEmail))
  }
)
