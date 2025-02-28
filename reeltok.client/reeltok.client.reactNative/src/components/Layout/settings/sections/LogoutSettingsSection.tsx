import useTranslation from '../../../../hooks/useTranslations'
import CustomButton from '../../../input/CustomButton'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React from 'react'

const LogoutSettingsSection: React.FC = () => {
  const t = useTranslation()

  return (
    <SettingsSection>
      <Setting>
        <CustomButton title={t('settings.logout')} widthPercentage={0.8} onPress={() => {}} />
      </Setting>
    </SettingsSection>
  )
}

export default LogoutSettingsSection
