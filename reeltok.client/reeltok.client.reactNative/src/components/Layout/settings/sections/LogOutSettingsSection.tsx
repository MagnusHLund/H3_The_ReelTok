import React from 'react'
import SettingsSection from '../SettingsSection'
import CustomButton from '../../../input/CustomButton'
import Setting from '../Setting'

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
