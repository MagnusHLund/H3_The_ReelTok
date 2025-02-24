import { DropdownOption } from './../../components/input/CustomDropdown'
import { changeLanguage, LanguageName, LocaleCode } from '../slices/settingsSlice'
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
