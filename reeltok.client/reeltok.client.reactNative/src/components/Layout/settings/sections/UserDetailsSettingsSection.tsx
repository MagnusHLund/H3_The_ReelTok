import CustomTextInput from '../../../input/CustomTextInput'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React from 'react'

const UserDetailsSettingsSection: React.FC = () => {
  // TODO: Hook up to global state
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
