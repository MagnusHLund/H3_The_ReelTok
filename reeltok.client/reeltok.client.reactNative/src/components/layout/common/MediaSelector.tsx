import useAppNavigation from '../../../hooks/useAppNavigation'
import MaterialIcons from '@expo/vector-icons/MaterialIcons'
import mediaPicker from '../../../utils/mediaPickerUtils'
import CustomButton from '../../input/CustomButton'
import { StyleSheet, View, Modal, TouchableWithoutFeedback } from 'react-native'
import Entypo from '@expo/vector-icons/Entypo'
import Gradient from './GradientBackground'
import Camera from '../camera/Camera'
import React, { useState } from 'react'
import { useDispatch } from 'react-redux'
import { setUploadedVideo, UploadedVideo } from '../../../redux/slices/uploadSlice'
import useAppSelector from '../../../hooks/useAppSelector'

interface MediaSelectorProps {
  handleSelectMedia: () => void
}

const MediaSelector: React.FC<MediaSelectorProps> = ({ handleSelectMedia }) => {
  const [selectedMedia, setSelectedMedia] = useState<string>()
  const [showCamera, setShowCamera] = useState(false)
  const navigateToScreen = useAppNavigation()
  const dispatch = useDispatch()
  const uploadedVideo: UploadedVideo = {
    title: '',
    description: '',
    category: { label: '', value: '' },
    fileUri: `${useAppSelector((state) => state.upload.video)}`
  }
  const handlePickMedia = async () => {
    try {
      const media: UploadedVideo = {
        title: '',
        description: '',
        category: { label: '', value: '' },
        fileUri: `${await mediaPicker()}`
      }
      if (media.fileUri != null) {
        dispatch(setUploadedVideo(media))
        setSelectedMedia(uploadedVideo.fileUri)
        navigateToScreen('UploadVideo')
      }
    } catch (error) {
      throw new Error(error.message)
    }
  }

  const handleShowCamera = () => {
    setShowCamera(true)
  }

  const handleHideCamera = () => {
    setShowCamera(false)
    handleSelectMedia()
  }

  return (
    <View style={styles.overlay}>
      {showCamera ? (
        <Camera cameraMode="video" onClose={handleHideCamera} />
      ) : (
        <View style={styles.outerContainer}>
          <Gradient colors={['transparent', 'transparent', 'black', 'black']}>
            <View style={styles.innerContainer}>
              <CustomButton widthPercentage={0.45} onPress={handleShowCamera}>
                <Entypo name="camera" size={24} color="white" />
              </CustomButton>
              <CustomButton
                widthPercentage={0.45}
                onPress={() => {
                  handlePickMedia()
                  handleSelectMedia()
                }}
              >
                <MaterialIcons name="photo-library" size={24} color="white" />
              </CustomButton>
            </View>
          </Gradient>
        </View>
      )}
    </View>
  )
}

const styles = StyleSheet.create({
  overlay: {
    flex: 1,
    justifyContent: 'flex-start',
    zIndex: 1,
  },
  outerContainer: {
    backgroundColor: 'transparent',
    width: '100%',
    position: 'absolute',
    bottom: '10%',
  },
  innerContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    backgroundColor: '#262626',
    width: '100%',
    borderRadius: 10,
    paddingLeft: 5,
    paddingRight: 5,
    paddingTop: 10,
    paddingBottom: 15,
    borderColor: '#ff0050',
    borderWidth: 1,
  },
})

export default MediaSelector
