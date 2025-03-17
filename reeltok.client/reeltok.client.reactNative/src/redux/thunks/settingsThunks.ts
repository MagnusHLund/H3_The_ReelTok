import {
  changeUsername,
  changeEmail,
  changeCategory,
  changeLanguage,
  Language,
} from '../slices/settingsSlice'
import { createAsyncThunk } from '@reduxjs/toolkit'

export const changeLanguageThunk = createAsyncThunk(
  'settings/changeLanguageThunk',
  async (language: Language, { dispatch }) => {
    dispatch(changeLanguage(language))
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
