import useTranslation from '../../../../hooks/useTranslations'
import CustomDropdown, { DropdownOption } from '../../../input/CustomDropdown'
import { changeLanguageThunk } from '../../../../redux/thunks/settingsThunks'
import { Language } from '../../../../redux/slices/settingsSlice'
import useAppSelector from '../../../../hooks/useAppSelector'
import useAppDispatch from '../../../../hooks/useAppDispatch'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React from 'react'

const languages = [
  { key: 0, label: 'English', value: 'en_GB' },
  { key: 1, label: 'Danish', value: 'da_DK' },
]

const LanguageSettingsSection: React.FC = () => {
  const language = useAppSelector((state) => state.settings.language)
  const dispatch = useAppDispatch()
  const t = useTranslation()

  const handleChangeLanguage = (selectedOption: DropdownOption) => {
    dispatch(
      changeLanguageThunk({
        LanguageName: selectedOption.label,
        locale: selectedOption.value,
      } as Language)
    )
  }

  const defaultOption: DropdownOption = {
    label: language.LanguageName,
    value: language.locale,
    key: 0,
  }

  return (
    <SettingsSection title={t('settings.language')}>
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
