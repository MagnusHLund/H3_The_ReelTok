import CustomDropdown, { DropdownOption } from '../../../input/CustomDropdown'
import { changeLanguageThunk } from '../../../../redux/thunks/settingsThunks'
import { useAppSelector } from '../../../../hooks/useAppSelector'
import { useAppDispatch } from '../../../../hooks/useAppDispatch'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React from 'react'

const languages = [
  { label: 'English', value: 'en_GB' },
  { label: 'Danish', value: 'da_DK' },
]

const LanguageSettingsSection: React.FC = () => {
  const language = useAppSelector((state) => state.settings.language)
  const dispatch = useAppDispatch()

  const handleChangeLanguage = (selectedOption: DropdownOption) => {
    dispatch(changeLanguageThunk(selectedOption))
  }

  const defaultOption: DropdownOption = {
    label: language.LanguageName,
    value: language.locale,
  }

  return (
    <SettingsSection title="Language">
      <Setting>
        <CustomDropdown
          options={languages}
          defaultOption={defaultOption}
          onChange={(selectedOption) => handleChangeLanguage(selectedOption)}
        />
      </Setting>
    </SettingsSection>
  )
}

export default LanguageSettingsSection
