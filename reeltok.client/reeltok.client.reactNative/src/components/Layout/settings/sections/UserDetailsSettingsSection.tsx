import React from 'react'
import CustomTextInput from '../../../input/CustomTextInput'
import Setting from '../Setting'
import SettingsSection from '../SettingsSection'

const UserDetailsSettingsSection: React.FC = () => {
  return (
    <SettingsSection title="User details" displayDivider={false}>
      <Setting title="Username">
        <CustomTextInput placeholder="Username" />
      </Setting>
      <Setting title="Email">
        <CustomTextInput placeholder="Email" />
      </Setting>
    </SettingsSection>
  )
}

export default UserDetailsSettingsSection
