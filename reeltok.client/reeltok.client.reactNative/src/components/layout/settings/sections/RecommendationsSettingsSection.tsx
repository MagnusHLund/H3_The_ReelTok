import { changeCategoryThunk } from '../../../../redux/thunks/settingsThunks'
import useTranslation from '../../../../hooks/useTranslations'
import useAppSelector from '../../../../hooks/useAppSelector'
import useAppDispatch from '../../../../hooks/useAppDispatch'
import CustomDropdown from '../../../input/CustomDropdown'
import SettingsSection from '../SettingsSection'
import Setting from '../Setting'
import React from 'react'

const categories = [
  { label: 'genre.gaming', value: 'Gaming' },
  { label: 'genre.tech', value: 'Tech' },
  { label: 'genre.dance', value: 'Dance' },
  { label: 'genre.fight', value: 'Fight' },
  { label: 'genre.sport', value: 'Sport' },
  { label: 'genre.comedy', value: 'Comedy' },
]

const RecommendationsSettingsSection: React.FC = () => {
  const selectedCategory = useAppSelector((state) => state.settings.selectedCategory)
  const dispatch = useAppDispatch()
  const t = useTranslation()

  const handleChangeCategory = (selectedOption: { value: string }) => {
    dispatch(changeCategoryThunk(selectedOption.value))
  }

  // TODO: Lets have a talk about default option for recommendation. I don't think its required.
  const defaultOption = {
    label: selectedCategory || t('settings.selectCategory'),
    value: selectedCategory || '',
  }

  return (
    <SettingsSection title={t('settings.recommendations')}>
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
