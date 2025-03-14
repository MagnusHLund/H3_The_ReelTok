import useAppNavigation from '../../../hooks/useAppNavigation'
import MaterialIcons from '@expo/vector-icons/MaterialIcons'
import mediaPicker from '../../../utils/mediaPickerUtils'
import CustomButton from '../../input/CustomButton'
import { StyleSheet, View, Modal, TouchableWithoutFeedback } from 'react-native'
import Entypo from '@expo/vector-icons/Entypo'
import Gradient from './GradientBackground'
import Camera from '../camera/Camera'
import React, { useState } from 'react'

interface mediaSelectorProps {
  handleSelectMedia: () => void
}

const MediaSelector: React.FC<mediaSelectorProps> = ({ handleSelectMedia }) => {
  const [selectedMedia, setSelectedMedia] = useState<string>()
  const [showCamera, setShowCamera] = useState(false)
  const navigateToScreen = useAppNavigation()

  const handlePickMedia = async () => {
    try {
      const media = await mediaPicker()
      if (media) {
        setSelectedMedia(media)
        navigateToScreen('UploadVideo', { video: media })
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
    <Modal transparent={true} onRequestClose={handleSelectMedia}>
      <TouchableWithoutFeedback onPress={handleSelectMedia}>
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
      </TouchableWithoutFeedback>
    </Modal>
  )
}

const styles = StyleSheet.create({
  overlay: {
    flex: 1,
    justifyContent: 'flex-end',
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
