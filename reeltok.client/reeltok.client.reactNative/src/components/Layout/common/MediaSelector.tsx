import { NativeStackNavigationProp } from '@react-navigation/native-stack'
import MaterialIcons from '@expo/vector-icons/MaterialIcons'
import mediaPicker from '../../../utils/mediaPickerUtils'
import { useNavigation } from '@react-navigation/native'
import CustomButton from '../../input/CustomButton'
import { StyleSheet, View } from 'react-native'
import Entypo from '@expo/vector-icons/Entypo'
import Gradient from './GradientBackground'
import React, { useState } from 'react'
import useAppNavigation from '../../../hooks/useAppNavigation'


interface mediaSelectorProps {
  handleSelectMedia: () => void
}

const MediaSelector: React.FC<mediaSelectorProps> = ({ handleSelectMedia }) => {
  const [selectedMedia, setSelectedMedia] = useState<string>()
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
  return (
    <View style={styles.outerContainer}>
      <Gradient colors={['transparent', 'transparent', 'black', 'black']}>
        <View style={styles.innerContainer}>
          <CustomButton
            widthPercentage={0.45}
            onPress={() => {
              navigateToScreen('Camera')
              handleSelectMedia()
            }}
          >
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
  )
}

const styles = StyleSheet.create({
  outerContainer: {
    backgroundColor: 'transparent',
    width: '100%',
    position: 'absolute',
    bottom: 0,
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
