import React from 'react'
import SettingsSection from '../SettingsSection'
import CustomDropdown from '../../../input/CustomDropdown'
import Setting from '../Setting'

const languages = [
  { label: 'English', value: 'en_GB' },
  { label: 'Danish', value: 'da_DK' },
]

const LanguageSettingsSection: React.FC = () => {
  return (
    <SettingsSection title="Language">
      <Setting>
        <CustomDropdown options={languages} placeholder="English" onChange={() => {}} />
      </Setting>
    </SettingsSection>
  )
}

export default LanguageSettingsSection
