import CustomDropdown, { DropdownOption } from '../../input/CustomDropdown'
import UploadedVideo from '../../layout/upload/UploadedVideo'
import CustomTextInput from '../../input/CustomTextInput'
import useAppSelector from '../../../hooks/useAppSelector'
import { useRoute } from '@react-navigation/native'
import CustomButton from '../../input/CustomButton'
import Section from '../../layout/common/Section'
import { View, StyleSheet } from 'react-native'
import Title from '../../layout/common/Title'
import { useState } from 'react'

const UploadVideo: React.FC = ({}) => {
  const route = useRoute()
  const uploadedVideo = useAppSelector((state) => state.upload.video)

  const [selectedCategory, setSelectedCategory] = useState<DropdownOption>()

  const handleChangeCategory = (selectedCategory: DropdownOption) => {
    setSelectedCategory({ label: selectedCategory.label, value: selectedCategory.value })
  }

  const categories: DropdownOption[] = [
    { label: 'Gaming', value: 'Gaming' },
    { label: 'Tech', value: 'Tech' },
    { label: 'Dance', value: 'Dance' },
    { label: 'Fight', value: 'Fight' },
    { label: 'Sport', value: 'Sport' },
    { label: 'Comedy', value: 'Comedy' },
  ]

  const defaultOption: DropdownOption = {
    label: 'Gaming',
    value: 'Gaming',
  }

  const styles = StyleSheet.create({
    videoScreenContainer: {
      height: '83.025%',
      width: '100%',
      display: 'flex',
      alignItems: 'center',
    },
    videoContainer: {
      marginTop: '5%',
      height: '30%',
      width: '100%',
    },
  })

  return (
    <>
      <View style={styles.videoContainer}>
        <UploadedVideo uri={uploadedVideo.fileUri} />
      </View>
      <View style={styles.videoScreenContainer}>
        <Section displayDivider={false}>
          <Title title="Title">
            <CustomTextInput placeholder="Title" />
          </Title>
        </Section>
        <Section displayDivider={false}>
          <Title title="Description">
            <CustomTextInput placeholder="Description" />
          </Title>
        </Section>
        <Section displayDivider={false}>
          <Title title="Category">
            <CustomDropdown
              defaultOption={defaultOption}
              options={categories}
              onChange={(selectedCategory: DropdownOption) =>
                handleChangeCategory(selectedCategory)
              }
            />
          </Title>
        </Section>
        <Section displayDivider={false}>
          <CustomButton
            widthPercentage={0.8}
            title="Upload"
            onPress={() => console.log('uploaded video' + uploadedVideo.fileUri)}
          />
        </Section>
      </View>
    </>
  )
}

export default UploadVideo
