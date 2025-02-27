import CustomDropdown from '../../../input/CustomDropdown'
import { changeCategoryThunk } from '../../../../redux/thunks/settingsThunks'
import useAppSelector from '../../../../hooks/useAppSelector'
import useAppDispatch from '../../../../hooks/useAppDispatch'
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
  const selectedCategory = useAppSelector((state) => state.settings.selectedCategory)
  const dispatch = useAppDispatch()

  const handleChangeCategory = (selectedOption: { value: string }) => {
    dispatch(changeCategoryThunk(selectedOption.value))
  }

  const defaultOption = {
    label: selectedCategory || 'Select Category',
    value: selectedCategory || '',
  }

  return (
    <SettingsSection title="Recommendation">
      <Setting>
        <CustomDropdown
          options={categories}
          defaultOption={defaultOption}
          onChange={(selectedOption) => handleChangeCategory(selectedOption)}
        />
      </Setting>
    </SettingsSection>
  )
}

export default RecommendationsSettingsSection
