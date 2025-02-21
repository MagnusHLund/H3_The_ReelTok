import React from 'react'
import SettingsSection from '../SettingsSection'
import CustomDropdown from '../../../input/CustomDropdown'
import Setting from '../Setting'

const categories = [
  { label: 'Gaming', value: 'Gaming' },
  { label: 'Tech', value: 'Tech' },
  { label: 'Dance', value: 'Dance' },
  { label: 'Fight', value: 'Fight' },
  { label: 'Sport', value: 'Sport' },
  { label: 'Comedy', value: 'Comedy' },
]

const RecommendationsSettingsSection: React.FC = () => {
  return (
    <SettingsSection title="Recommendation">
      <Setting>
        <CustomDropdown options={categories} placeholder="Gaming" onChange={() => {}} />
      </Setting>
    </SettingsSection>
  )
}

export default RecommendationsSettingsSection
