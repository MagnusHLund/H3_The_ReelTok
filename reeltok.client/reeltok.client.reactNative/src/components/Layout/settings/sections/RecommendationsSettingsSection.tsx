import CustomDropdown from '../../../input/CustomDropdown'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React from 'react'

const categories = [
  { label: 'Gaming', value: 'Gaming' },
  { label: 'Tech', value: 'Tech' },
  { label: 'Dance', value: 'Dance' },
  { label: 'Fight', value: 'Fight' },
  { label: 'Sport', value: 'Sport' },
  { label: 'Comedy', value: 'Comedy' },
]

const RecommendationsSettingsSection: React.FC = () => {
  // TODO: Hook up to global state
  return (
    <SettingsSection title="Recommendation">
      <Setting>
        <CustomDropdown options={categories} onChange={() => {}} />
      </Setting>
    </SettingsSection>
  )
}

export default RecommendationsSettingsSection
