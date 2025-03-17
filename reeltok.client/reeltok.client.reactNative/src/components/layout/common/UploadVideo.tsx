import CustomDropdown, { DropdownOption } from '../../input/CustomDropdown'
import UploadedMedia from '../upload/UploadedMedia'
import CustomTextInput from '../../input/CustomTextInput'
import useAppSelector from '../../../hooks/useAppSelector'
import { useRoute } from '@react-navigation/native'
import CustomButton from '../../input/CustomButton'
import Section from '../../layout/common/Section'
import { View, StyleSheet } from 'react-native'
import Title from '../../layout/common/Title'
import { useEffect, useState } from 'react'
import useTranslation from '../../../hooks/useTranslations'
import { setUploadedVideoThunk } from '../../../redux/thunks/uploadThunks'
import type { UploadedVideo } from '../../../redux/slices/uploadSlice'
import { useDispatch } from 'react-redux'

export const isImage = (uri: string) => {
  const imageExtensions = ['.jpg', '.jpeg', '.png', '.gif']
  return imageExtensions.some((ext) => uri.toLowerCase().endsWith(ext))
}

const UploadVideo: React.FC = ({}) => {
  const t = useTranslation() 
  const [uploadedVideoTitle, setUploadedVideoTitle] = useState<string>('')
  const [uploadedVideoDescription, setUploadedVideoDescription] = useState<string>('')
  const [selectedCategory, setSelectedCategory] = useState<DropdownOption>({label: '', value: ''})
  const dispatch = useDispatch()
  const uploadedVideo: UploadedVideo = {
    title: uploadedVideoTitle,
    description: uploadedVideoDescription,
    category: { label: selectedCategory?.label, value: selectedCategory.value },
    fileUri: useAppSelector((state) => state.upload.video.fileUri),
  } 
  
  console.log(uploadedVideo.fileUri)

  const handleChangeTitle = (text: string) => {
    setUploadedVideoTitle(text)
  }

  const handleChangeDescription = (text: string) => {
    setUploadedVideoDescription(text)
  }

  const handleChangeCategory = (selectedCategory: DropdownOption) => {
    setSelectedCategory({ label: selectedCategory.label, value: selectedCategory.value })
  } 

  const handleUpload = () => {
    dispatch(setUploadedVideoThunk(uploadedVideo)) 
  }

  const categories: DropdownOption[] = [
    { label: t('genre.gaming'), value: 'Gaming' },
    { label: t('genre.tech'), value: 'Tech' },
    { label: t('genre.dance'), value: 'Dance' },
    { label: t('genre.fight'), value: 'Fight' },
    { label: t('genre.sport'), value: 'Sport' },
    { label: t('genre.comedy'), value: 'Comedy' },
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
        <UploadedMedia uri={uploadedVideo.fileUri} />
      </View>
      <View style={styles.videoScreenContainer}>
        {!isImage(uploadedVideo.fileUri) && (
          <>
            <Section displayDivider={false}>
              <Title title="Title">
                <CustomTextInput placeholder="Title" onChange={(value) => handleChangeTitle(value)}/>
              </Title>
            </Section>
            <Section displayDivider={false}>
              <Title title="Description">
                <CustomTextInput placeholder="Description" onChange={(value) => handleChangeDescription(value)} />
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
          </>
        )}
        <Section displayDivider={false}>
          <CustomButton
            widthPercentage={0.8}
            title="Upload"
            onPress={() => handleUpload()}
          />
        </Section>
      </View>
    </>
  )
}

export default UploadVideo
