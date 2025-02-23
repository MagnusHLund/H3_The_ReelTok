import CustomButton from '../../../input/CustomButton'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React from 'react'

const RecommendationsSettingsSection: React.FC = () => {
  return (
    <SettingsSection>
      <Setting>
        <CustomButton title="Logout" widthPercentage={0.8} onPress={() => {}} />
      </Setting>
    </SettingsSection>
  )
}

export default RecommendationsSettingsSection
