import { changeEmailThunk, changeUsernameThunk } from '../../../../redux/thunks/settingsThunks'
import useTranslation from '../../../../hooks/useTranslations'
import useAppSelector from '../../../../hooks/useAppSelector'
import useAppDispatch from '../../../../hooks/useAppDispatch'
import CustomTextInput from '../../../input/CustomTextInput'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React, { useState } from 'react'

const UserDetailsSettingsSection: React.FC = () => {
  const dispatch = useAppDispatch()
  const username = useAppSelector((state) => state.settings.username)
  const email = useAppSelector((state) => state.settings.email)
  const t = useTranslation()

  const [newUsername, setNewUsername] = useState(username)
  const [newEmail, setNewEmail] = useState(email)

  const handleChangeUsername = () => {
    dispatch(changeUsernameThunk(newUsername)) // Dispatch action to change username
  }

  const handleChangeEmail = () => {
    dispatch(changeEmailThunk(newEmail)) // Dispatch action to change email
  }

  return (
    <SettingsSection title={t('settings.userDetails')} displayDivider={false}>
      <Setting title={t('settings.username')}>
        <CustomTextInput
          placeholder={newUsername} // Placeholder is used, will update based on input
          // TODO: Later add onChangeText or similar to bind input to state
        />
      </Setting>
      <Setting title={t('settings.email')}>
        <CustomTextInput
          placeholder={newEmail} // Placeholder is used, will update based on input
          // TODO: Later add onChangeText or similar to bind input to state
        />
      </Setting>
    </SettingsSection>
  )
}

export default UserDetailsSettingsSection
